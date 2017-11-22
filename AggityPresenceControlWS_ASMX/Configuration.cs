using System.Configuration;

namespace AggityPresenceControlWS_ASMX
{
    public static class Configuration
    {
        public static string DATABASE_FILE { get; private set; } = ConfigurationManager.AppSettings["DATABASE_FILE"];
    }
}