using AggityPresenceControlWS_ASMX.GTTRestClient.Model;
using AggityPresenceControlWS_ASMX.Helpers;
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
        [WebMethod]
        public bool SendPunchData(string punchDataBaseJson)
        {
            try
            {
                PunchData punchData = JsonConvert.DeserializeObject<PunchData>(punchDataBaseJson);
                punchData.Exported = false;
                punchData.ExportedTime = DateTime.MinValue;
                punchData.Time = punchData.Time.ToLocalTime();
                return Database.DatabaseManager.InsertDataIfNew(punchData);
            }
            catch (Exception ex)
            {
                LogManager.Logger.Error("Exception in SendPunchData WS method", ex);
                return false;
            }
        }

        [WebMethod]
        public void ExportPunchData()
        {
            Database.DatabaseManager.GetNotExportedPunchData().ForEach((p) =>
            {
                bool saved = AsyncHelpers.RunSync<bool>(() => GTTRestClient.GTTRestClient.SendPcRaw(new PcRaw(p)));
                if (saved)
                {
                    Database.DatabaseManager.SetExportedData(p.IdRow);
                }
            });
        }
    }
}
