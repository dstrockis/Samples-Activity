using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samples_Activity.Models
{
    public class Participation
    {
        public int Id { get; set; }
        public int test { get; set; }

        public class CommitsByWeek
        {
            public int Id { get; set; }
            public int quantity { get; set; } 
        }

        public ICollection<CommitsByWeek> Owner { get; set; }
        public ICollection<CommitsByWeek> All { get; set; }
    }
}