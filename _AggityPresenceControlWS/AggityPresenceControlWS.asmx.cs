using AggityPresenceControlDataModel;
using AggityPresenceControlInterfaces;
using AggityPresenceControlWS.Database;
using System;
using System.Web.Services;

namespace AggityPresenceControlWS
{
    /// <summary>
    /// Descripción breve de AggityPresenceControlWS
    /// </summary>
    [WebService(Namespace = "http://aggity.presencecontrol.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class AggityPresenceControlWS : WebService, IPunchDataSend
    {
        /*
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }
        */

        [WebMethod]
        bool IPunchDataSend.SendPunchData(PunchDataBase punchData)
        {
            try
            {
                return DatabaseManager.InsertDataIfNew(new Database.PunchData(punchData));
            }
            catch (Exception ex)
            {
                LogManager.Logger.Error("Exception inserting data if new: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return false;
        }
    }
}
