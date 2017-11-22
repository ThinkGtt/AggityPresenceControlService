using AggityPresenceControlDataModel;
using AggityPresenceControlDataModel.Database;
using SQLite.Net.Attributes;
using System;

namespace AggityPresenceControlService.Database
{
    public class PunchData : PunchDataBase, IStorable
    {
        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }

        public override string TerminalId { get; set; }        
        public override string CardUid { get; set; }
        public override DateTime Time
        {
            get { return time; }
            set
            {
                if(value != null)
                {
                    time = value.ToUniversalTime();
                }
            }
        }

        private DateTime time;
    }
}
