using log4net;

namespace AggityPresenceControlWS
{
    public static class LogManager
    {
        public static readonly ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
