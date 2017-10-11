using log4net;

namespace AggityPresenceControlService
{
    public static class LogManager
    {
        public static readonly ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
