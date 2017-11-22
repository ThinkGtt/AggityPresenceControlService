using log4net;

namespace AggityPresenceControlWS_ASMX
{
    public static class LogManager
    {
        static LogManager()
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger.Info("Starting log4net");
        }

        public static readonly ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}