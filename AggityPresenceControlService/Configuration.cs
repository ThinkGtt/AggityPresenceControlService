using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggityPresenceControlService
{
    public static class Configuration
    {
        public static string DATABASE_FILE { get; private set; } = ConfigurationManager.AppSettings["DATABASE_FILE"];
        public static string WEBSERVICE_URL { get; private set; } = ConfigurationManager.AppSettings["WEBSERVICE_URL"];
        public static string TERMINAL_ID { get; private set; } = ConfigurationManager.AppSettings["TERMINAL_ID"];        
    }
}
