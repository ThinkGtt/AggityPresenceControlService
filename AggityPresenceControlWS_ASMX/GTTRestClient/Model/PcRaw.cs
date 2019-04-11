using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AggityPresenceControlWS_ASMX.GTTRestClient.Model
{
    public class PcRaw : GttRestDataBase<PcRaw.PcRawModel>
    {
        public PcRaw(Models.PunchData data)
        {
            this.data.RawCardCode = data.CardUid.Replace("-", "");
            this.data.RawTerminal = data.TerminalId;
            this.data.RawMarkDateTime = (RawMarkDateTime)data.Time;
        }

        public class PcRawModel
        {
            public string RawCardCode { get; set; }
            public string RawTerminal { get; set; }
            public RawMarkDateTime RawMarkDateTime { get; set; }
        }
    }
}