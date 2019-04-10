using AggityPresenceControlService.Database;
using PCSCLib;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace AggityPresenceControlService
{
    static class Program
    {
        static SemaphoreSlim sl = new SemaphoreSlim(1);

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
                while (true)
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
                        await sl.WaitAsync();
                        using (DatabaseManager dbm = new DatabaseManager())
                        {
                            await dbm.AddData<PunchData>(
                                new PunchData()
                                {
                                    TerminalId = Configuration.TERMINAL_ID,
                                    CardUid = e.UID,
                                    Time = DateTime.Now
                                }
                            );
                        }
                    }
                    finally
                    {
                        sl.Release();
                    }
                }
                else
                {
                    Console.WriteLine("Event called. UID is not valid");
                    LogManager.Logger.Info("Event called. UID is not valid");
                }
            };
        }

        internal static void Stop()
        {
            // onstop code here
            pcsc?.Dispose();
            pcsc = null;
        }
    }
}
