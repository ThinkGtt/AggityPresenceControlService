using AggityPresenceControlWS_ASMX.GTTRestClient.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AggityPresenceControlWS_ASMX.GTTRestClient
{
    public class GTTRestClient
    {
        static string GTT_BASE_PATH = ConfigurationManager.AppSettings["GTT_BASE_PATH"];
        static string GTT_REST_AUTHORIZATION { get; set; } = ConfigurationManager.AppSettings["GTT_REST_AUTHORIZATION"];

        public static async Task<bool> SendPcRaw(PcRaw raw)
        {
            try
            {
                /*
                string data = raw.ToJsonString();
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(Configuration.GTT_PCRAW_URL)
                };
                client.DefaultRequestHeaders.TryAddWithoutValidation("gtt_user", Configuration.GTT_USER);
                client.DefaultRequestHeaders.TryAddWithoutValidation("gtt_token", Configuration.GTT_TOKEN);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, Configuration.GTT_PCRAW_URL);
                req.Content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.SendAsync(req);
                httpResponseMessage.EnsureSuccessStatusCode();
                HttpContent httpContent = httpResponseMessage.Content;
                string responseString = await httpContent.ReadAsStringAsync();

                Consequence consequence = JsonConvert.DeserializeObject<Consequence>(responseString);
                //var cons = consequence?.consequence?.GetChildConsecuences();
                var doIt = consequence.consequence.GetDoIt();

                //Console.WriteLine(responseString);
                return doIt;
                */
                //var dateTime = new DateTime(raw.data.RawMarkDateTime.year, raw.data.RawMarkDateTime.month, raw.data.RawMarkDateTime.dayofMonth, raw.data.RawMarkDateTime.hourOfDay, raw.data.RawMarkDateTime.minute, raw.data.RawMarkDateTime.second, raw.data.RawMarkDateTime.millisecond, DateTimeKind.Local);
                var dateTime = new DateTime(raw.data.RawMarkDateTime.year, raw.data.RawMarkDateTime.month, raw.data.RawMarkDateTime.dayofMonth, raw.data.RawMarkDateTime.hourOfDay, raw.data.RawMarkDateTime.minute, raw.data.RawMarkDateTime.second, raw.data.RawMarkDateTime.millisecond, DateTimeKind.Utc);
                var markApi = new IO.Swagger.Api.CardreadermarkApi(GTT_BASE_PATH);
                markApi.Configuration.AddApiKey("Authorization", GTT_REST_AUTHORIZATION);
                markApi.Mark(new IO.Swagger.Model.CardReaderUserMarkEvent(raw.data.RawCardCode, dateTime, raw.data.RawTerminal));
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
