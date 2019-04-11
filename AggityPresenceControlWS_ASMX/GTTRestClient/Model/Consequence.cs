using System;
using System.Collections.Generic;
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
            public dynamic Consequences { get; set; }
        }
    }
}