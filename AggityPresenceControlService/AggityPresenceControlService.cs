using AggityPresenceControlService.Database;
using PCSCLib;
using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace AggityPresenceControlService
{
    partial class AggityPresenceControlService : ServiceBase
    {
        public static string SERVICE_NAME {get; } = "AggityPresenceControlService";

        public AggityPresenceControlService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: agregar código aquí para iniciar el servicio.
            Program.Start(args);
        }

        protected override void OnStop()
        {
            // TODO: agregar código aquí para realizar cualquier anulación necesaria para detener el servicio.
            Program.Stop();
        }



        static SemaphoreSlim sl = new SemaphoreSlim(1);
        static bool RunningService { get; set; } = true;


        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
                // running as service
                using (var service = new AggityPresenceControlService())
                {
                    ServiceBase.Run(service);
                }
            else
            {
                if (args.Length == 1)
                {
                    switch (args[0])
                    {
                        case "-install":
                            InstallService();
                            StartService();
                            break;
                        case "-uninstall":
                            StopService();
                            UninstallService();
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    return;
                }
                // running as console app
                Start(args);
                Console.WriteLine("Press any key to stop...");
                LogManager.Logger.Info("Starting...");
                //Console.ReadKey(true);
                Thread.Sleep(1200000);

                Stop();
            }
        }

        static PCSCLibUID pcsc = null;
        internal static void Start(string[] args)
        {
            Task.Run(async () =>
            {
                while (RunningService)
                {
                    using (DatabaseManager dbm = new DatabaseManager())
                    {
                        await dbm.SynchronizeOfflineData(async (PunchData p) =>
                        {
                            var client = new AggityPresenceControlWSAsmxClient.AggityPresenceControlWSAsmxClient(Configuration.WEBSERVICE_URL);
                            return await client.SendPunchData(p);
                        });
                    }
                    await Task.Delay(5000);
                }
            });


            // onstart code here
            pcsc = new PCSCLibUID();
            pcsc.PCSCCardUIDLoaded += async (o, e) =>
            {
                if (e.SW1 == "90" && e.SW2 == "00")
                {
                    Console.WriteLine("Event called. UID = " + e.UID);
                    LogManager.Logger.Info("Event called. UID = " + e.UID);

                    try
                    {
                        pcsc.DisableBuzzerOnCardDetected(e.ReaderName);
                        bool resultBeep = pcsc.PlayStatus(e.ReaderName, true);
                        if (!resultBeep)
                        {
                            return;
                        }
                        await sl.WaitAsync();
                        using (DatabaseManager dbm = new DatabaseManager())
                        {
                            bool result = await dbm.AddData<PunchData>(
                                new PunchData()
                                {
                                    TerminalId = Configuration.TERMINAL_ID,
                                    CardUid = e.UID,
                                    Time = DateTime.Now.ToUniversalTime()
                                }
                            );
                            /*if(result)
                            {
                                pcsc.PlayWithLedsAndBuzzer(e.ReaderName);
                            }
                            */
                            //pcsc.PlayStatus(e.ReaderName, result);
                        }
                    }
                    finally
                    {
                        sl.Release();
                    }
                }
                else
                {
                    pcsc.PlayStatus(e.ReaderName, false);
                    Console.WriteLine("Event called. UID is not valid");
                    LogManager.Logger.Info("Event called. UID is not valid");
                }
            };
        }

        internal static void Stop()
        {
            // onstop code here
            RunningService = false;
            //Thread.Sleep(5000);
            pcsc?.Dispose();
            pcsc = null;
        }


        /****************** INSTALLER ******************/
        //https://stackoverflow.com/questions/1195478/how-to-make-a-net-windows-service-start-right-after-the-installation/1195621#1195621

        private static bool IsInstalled()
        {
            using (ServiceController controller =
                new ServiceController(SERVICE_NAME))
            {
                try
                {
                    ServiceControllerStatus status = controller.Status;
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        private static bool IsRunning()
        {
            using (ServiceController controller =
                new ServiceController(SERVICE_NAME))
            {
                if (!IsInstalled()) return false;
                return (controller.Status == ServiceControllerStatus.Running);
            }
        }

        private static AssemblyInstaller GetInstaller()
        {
            AssemblyInstaller installer = new AssemblyInstaller(
                typeof(ProjectInstaller).Assembly, null);
            installer.UseNewContext = true;
            return installer;
        }


        private static void InstallService()
        {
            if (IsInstalled()) return;

            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Install(state);
                        installer.Commit(state);
                    }
                    catch
                    {
                        try
                        {
                            installer.Rollback(state);
                        }
                        catch { }
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static void UninstallService()
        {
            if (!IsInstalled()) return;
            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Uninstall(state);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static void StartService()
        {
            if (!IsInstalled()) return;

            using (ServiceController controller =
                new ServiceController(SERVICE_NAME))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Running)
                    {
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running,
                            TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        private static void StopService()
        {
            if (!IsInstalled()) return;
            using (ServiceController controller =
                new ServiceController(SERVICE_NAME))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Stopped)
                    {
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped,
                             TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

    }
}
