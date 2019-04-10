using AggityPresenceControlDataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AggityPresenceControlWSClient
{
    public class AggityPresenceControlWSClient
    {
        string BaseUrl { get; set; }

        public AggityPresenceControlWSClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<bool> SendPunchData(PunchDataBase punchData)
        {
            var uri = new Uri(string.Format(BaseUrl, string.Empty));
        
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;


            var json = JsonConvert.SerializeObject(punchData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
