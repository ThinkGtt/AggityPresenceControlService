using AggityPresenceControlWS_ASMX.GTTRestClient.Model;
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
        static string GTT_USER = ConfigurationManager.AppSettings["GTT_USER"];
        static string GTT_TOKEN = ConfigurationManager.AppSettings["GTT_TOKEN"];
        static string GTT_PCRAW_URL = ConfigurationManager.AppSettings["GTT_PCRAW_URL"];

        public static async Task<bool> SendPcRaw(PcRaw raw)
        {
            try
            {
                string data = raw.ToJsonString();
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(GTT_PCRAW_URL)
                };
                client.DefaultRequestHeaders.TryAddWithoutValidation("gtt_user", GTT_USER);
                client.DefaultRequestHeaders.TryAddWithoutValidation("gtt_token", GTT_TOKEN);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, GTT_PCRAW_URL);
                req.Content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.SendAsync(req);
                httpResponseMessage.EnsureSuccessStatusCode();
                HttpContent httpContent = httpResponseMessage.Content;
                string responseString = await httpContent.ReadAsStringAsync();

                Console.WriteLine(responseString);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
