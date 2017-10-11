using System;

namespace AggityPresenceControlDataModel
{
    public abstract class PunchDataBase
    {
        public virtual int Id { get; set; }
        public virtual string TerminalId { get; set; }
        public virtual string CardUid { get; set; }
        public virtual DateTime Time { get; set; }
    }
}
