using AggityPresenceControlDataModel;
using AggityPresenceControlUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AggityPresenceControlWSAsmxClient
{
    public class AggityPresenceControlWSAsmxClient
    {
        static string PRESHARED_KEY = ConfigurationManager.AppSettings["PRESHARED_KEY"];

        string BaseUrl { get; set; }        

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
                    //bypass certificate validation
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    PunchDataServiceWSProxy.PunchDataService proxy = new PunchDataServiceWSProxy.PunchDataService();
                    proxy.Url = BaseUrl;
                    //return proxy.SendPunchData(JsonConvert.SerializeObject(punchData));
                    //return proxy.SendPunchData(jsonData);
                    var jsonData = JsonConvert.SerializeObject(punchData);
                    var hash = CryptoUtils.ComputeSha256Hash(jsonData, PRESHARED_KEY);
                    return proxy.SendPunchData(jsonData, hash);
                }
                catch(Exception ex)
                {
                    return false;
                }
            });
        }
    }
}
