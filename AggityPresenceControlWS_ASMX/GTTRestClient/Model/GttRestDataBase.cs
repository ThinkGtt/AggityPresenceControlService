using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AggityPresenceControlWS_ASMX.GTTRestClient.Model
{
    public class GttRestDataBase<T> where T : new()
    {
        public T data { get; set; } = new T();


        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}