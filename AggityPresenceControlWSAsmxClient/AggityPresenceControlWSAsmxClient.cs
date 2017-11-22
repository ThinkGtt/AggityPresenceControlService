using AggityPresenceControlDataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggityPresenceControlWSAsmxClient
{
    public class AggityPresenceControlWSAsmxClient
    {
        string BaseUrl { get; set; }
        //HttpClient Client { get; set;  }

        public AggityPresenceControlWSAsmxClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<bool> SendPunchData(PunchDataBase punchData)
        {
            return await Task.Run<bool>(() =>
            {
                try
                {
                    PunchDataServiceWSProxy proxy = new PunchDataServiceWSProxy();
                    proxy.Url = BaseUrl;
                    return proxy.SendPunchData(JsonConvert.SerializeObject(punchData));
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}
