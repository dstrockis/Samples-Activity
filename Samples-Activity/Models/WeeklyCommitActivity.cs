using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samples_Activity.Models
{
    public class WeeklyCommitActivity
    {
        public class DayCount
        { 
            public int Id { get; set; }
            public int Count { get; set; }

            public DayCount() { }

            public DayCount(int count)
            { Count = count; }
        }
        public int Id { get; set; }
        public long Week { get; set; }
        public int Total { get; set; }
        public ICollection<DayCount> Days { get; set; }

        public WeeklyCommitActivity() { }

        public WeeklyCommitActivity(Octokit.WeeklyCommitActivity weeklyAct)
        {
            Week = weeklyAct.Week;
            Total = weeklyAct.Total;
            this.Days = new List<DayCount>();
            foreach (var count in weeklyAct.Days)
            {
                Days.Add(new DayCount(count));
            }
        }
    }
}