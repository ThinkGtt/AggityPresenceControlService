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
                Thread.Sleep(120000);

                Stop();
            }
        }

        static PCSCLibUID pcsc = null;
        internal static void Start(string[] args)
        {
            DatabaseManager dbm = new DatabaseManager();

            Task t1 = dbm.AddData<PunchData>(
                new PunchData()
                {
                    TerminalId = "1",
                    CardUid = "1234567890",
                    Time = DateTime.Now
                }
            );
            Task.WaitAll(t1);

            Task t2 = dbm.SynchronizeOfflineData(async (PunchData p) =>
            {
                //send data to service and check is received. Return true if received, return false elsewhere.
                //return await Task.FromResult<bool>(true);

                //AggityPresenceControlWSClient.AggityPresenceControlWSClient client = new AggityPresenceControlWSClient.AggityPresenceControlWSClient(Configuration.WEBSERVICE_URL);
                //return await client.SendPunchData(p);

                var client = new AggityPresenceControlWSAsmxClient.AggityPresenceControlWSAsmxClient(Configuration.WEBSERVICE_URL);
                return await client.SendPunchData(p);
            });
            Task.WaitAll(t2);

            return;

            // onstart code here
            pcsc = new PCSCLibUID();
            pcsc.PCSCCardUIDLoaded += (o, e) => 
            {
                if (e.SW1 == "90" && e.SW2 == "00")
                {
                    Console.WriteLine("Event called. UID = " + e.UID);
                }
                else
                {
                    Console.WriteLine("Event called. UID is not valid");
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
