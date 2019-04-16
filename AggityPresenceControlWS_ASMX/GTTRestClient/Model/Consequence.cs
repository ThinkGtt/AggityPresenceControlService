using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace AggityPresenceControlWS_ASMX.GTTRestClient.Model
{
    public class Consequence
    {
        public dynamic data { get; set; }
        public ConsequenceModel consequence { get; set; }

        public class ConsequenceModel
        {
            public bool DoIt { get; set; }
            public bool ForcedShowConsequence { get; set; }
            public string UserRelevantMessage { get; set; }
            public bool IsResponse { get; set; }
            public string Id { get; set; }
            public bool Result { get; set; }
            public string Caption { get; set; }
            public string Message { get; set; }
            public bool Unbreakable { get; set; }
            public bool DoAction { get; set; }
            public string Severity { get; set; }
            public string[] Parameters { get; set; }
            private JObject consequences;
            public JObject Consequences
            {
                get
                {
                    return consequences;
                }
                set
                {
                    consequences = value;                    
                }
            }

            public bool GetDoIt()
            {
                dynamic dynObject = ConvertJTokenToObject(consequences);
                return DoIt && InternalGetDoit(dynObject);
            }

            bool InternalGetDoit(dynamic dynObject)
            {
                if (dynObject == null)
                {
                    return true;
                }
                if (dynObject is ExpandoObject)
                {
                    var dic = (IDictionary<string, object>)((ExpandoObject)dynObject);
                    if(dic.Values.Count == 0)
                    {
                        return true;
                    }
                    var val = (dynamic)dic.Values.ElementAt(0);
                    return ((bool)val.DoIt && InternalGetDoit(val.Consequences));
                }
                else
                {
                    return false;
                }                
            }

            public object ConvertJTokenToObject(JToken token)
            {
                if (token is JValue)
                {
                    return ((JValue)token).Value;
                }
                if (token is JObject)
                {
                    ExpandoObject expando = new ExpandoObject();
                    (from childToken in ((JToken)token) where childToken is JProperty select childToken as JProperty).ToList().ForEach(property => {
                        ((IDictionary<string, object>)expando).Add(property.Name, ConvertJTokenToObject(property.Value));
                    });
                    return expando;
                }
                if (token is JArray)
                {
                    object[] array = new object[((JArray)token).Count];
                    int index = 0;
                    foreach (JToken arrayItem in ((JArray)token))
                    {
                        array[index] = ConvertJTokenToObject(arrayItem);
                        index++;
                    }
                    return array;
                }
                throw new ArgumentException(string.Format("Unknown token type '{0}'", token.GetType()), "token");
            }
        }
    }
}