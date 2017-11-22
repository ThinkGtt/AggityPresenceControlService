using AggityPresenceControlDataModel;
using AggityPresenceControlDataModel.Database;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AggityPresenceControlWS.Models
{
    public class PunchData : PunchDataBase, IStorable
    {
        [PrimaryKey]
        public string RowId
        {
            get
            {
                return string.Format($"{Id}-{TerminalId}-{CardUid}-{Time.Ticks}");
            }
        }

        public override int Id { get; set; }
        public override string TerminalId { get; set; }
        public override string CardUid { get; set; }
        public override DateTime Time
        {
            get { return time; }
            set
            {
                if (value != null)
                {
                    time = value.ToUniversalTime();
                }
            }
        }
        public bool Exported { get; set; } = false;
        public DateTime ExportedTime
        {
            get { return exportedTime; }
            set
            {
                if (value != null)
                {
                    exportedTime = value.ToUniversalTime();
                }
            }
        }

        private DateTime time;
        private DateTime exportedTime;

        public PunchData(PunchDataBase punchDataBase) : base()
        {
            Id = punchDataBase.Id;
            TerminalId = punchDataBase.TerminalId;
            CardUid = punchDataBase.CardUid;
            Time = punchDataBase.Time;
        }

        public PunchData() : base()
        {
        }
    }
}