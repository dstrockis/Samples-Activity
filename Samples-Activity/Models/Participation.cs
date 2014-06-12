using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samples_Activity.Models
{
    public class Participation
    {
        public int Id { get; set; }
        public int All { get; set; }
        public int Owner { get; set; }

        public Participation() { }
        public Participation(int All, int Owner)
        {
            this.All = All;
            this.Owner = Owner;
        }
    }
}