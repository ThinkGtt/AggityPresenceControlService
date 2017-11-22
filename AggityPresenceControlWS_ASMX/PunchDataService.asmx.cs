using AggityPresenceControlWS_ASMX.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AggityPresenceControlWS_ASMX
{
    /// <summary>
    /// Descripción breve de PunchDataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class PunchDataService : System.Web.Services.WebService
    {
        /*
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }
        */

        [WebMethod]
        public bool SendPunchData(string punchDataBaseJson)
        {
            try
            {
                PunchData punchData = JsonConvert.DeserializeObject<PunchData>(punchDataBaseJson);
                punchData.Exported = false;
                punchData.ExportedTime = DateTime.MinValue;
                return Database.DatabaseManager.InsertDataIfNew(punchData);
            }
            catch (Exception ex)
            {
                LogManager.Logger.Error("Exception in SendPunchData WS method", ex);
                return false;
            }
        }
    }
}
