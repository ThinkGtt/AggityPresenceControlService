using System.Configuration;

namespace AggityPresenceControlWS_ASMX
{
    public static class Configuration
    {
        public static string DATABASE_FILE { get; private set; } = ConfigurationManager.AppSettings["DATABASE_FILE"];

        public static string GTT_USER = ConfigurationManager.AppSettings["GTT_USER"];
        public static string GTT_TOKEN = ConfigurationManager.AppSettings["GTT_TOKEN"];
        public static string GTT_PCRAW_URL = ConfigurationManager.AppSettings["GTT_PCRAW_URL"];
        public static string GTT_PURGUE_EXPORTED_MARKS_AFTER_DAYS { get; private set; } = ConfigurationManager.AppSettings["GTT_PURGUE_EXPORTED_MARKS_AFTER_DAYS"];        
    }
}