using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AggityPresenceControlWS_ASMX.GTTRestClient.Model
{
    public class RawMarkDateTime
    {
        public int year { get; set; }
        public int month { get; set; }
        public int dayofMonth { get; set; }
        public int hourOfDay { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
        public int millisecond { get; set; }

        public static explicit operator RawMarkDateTime(DateTime dt)
        {
            return new RawMarkDateTime
            {
                year = dt.Year,
                month = dt.Month,
                dayofMonth = dt.Day,
                hourOfDay = dt.Hour,
                minute = dt.Minute,
                second = dt.Second,
                millisecond = dt.Millisecond
            };
        }
    }
}