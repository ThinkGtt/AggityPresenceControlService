using SQLite.Net.Attributes;
using System;

namespace AggityPresenceControlService.Database
{
    public class PunchData : IStorable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string TerminalId { get; set; }        
        public string CardUid { get; set; }
        public DateTime Time
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
