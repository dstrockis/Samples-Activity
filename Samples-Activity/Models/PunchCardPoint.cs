using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samples_Activity.Models
{
    public class PunchCardPoint
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int HourOfTheDay { get; set; }
        public int CommitCount { get; set; }

        public PunchCardPoint() { }
        public PunchCardPoint(DayOfWeek DoW, int HoD, int cCount)
        {
            DayOfWeek = DoW;
            HourOfTheDay = HoD;
            CommitCount = cCount;
        }
    }
}