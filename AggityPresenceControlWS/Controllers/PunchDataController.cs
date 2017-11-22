using AggityPresenceControlWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AggityPresenceControlWS.Controllers
{
    public class PunchDataController : ApiController
    {
        static Random rnd = new Random();

        /*
        // GET: api/PunchData
        public IEnumerable<PunchData> Get()
        {
            PunchData[] punchDatas = new PunchData[]
            {
                new PunchData
                {
                    Id = rnd.Next(),
                    CardUid = Guid.NewGuid().ToString("N"),
                    TerminalId = Guid.NewGuid().ToString("N"),
                    Time = DateTime.Now
                },
                new PunchData
                {
                    Id = rnd.Next(),
                    CardUid = Guid.NewGuid().ToString("N"),
                    TerminalId = Guid.NewGuid().ToString("N"),
                    Time = DateTime.Now
                }
            };
            //return punchDatas;
            throw new HttpResponseException(HttpStatusCode.OK);
        }

        // GET: api/PunchData/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PunchData
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PunchData/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PunchData/5
        public void Delete(int id)
        {
        }
        */

        // POST: api/PunchData
        public void Post([FromBody]PunchData punchData)
        {
            try
            {
                if ((punchData == null) || (punchData?.CardUid == null) || (punchData?.RowId == null) || (punchData?.TerminalId == null) || (punchData?.Time == null))
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                punchData.Exported = false;
                punchData.ExportedTime = DateTime.MinValue;
                if (!Database.DatabaseManager.InsertDataIfNew(punchData))
                {
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
            catch(Exception ex)
            {
                LogManager.Logger.Error("Exception: " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
