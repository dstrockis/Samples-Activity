using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samples_Activity.Models
{
    public class CodeFrequency
    {
        public int Id { get; set; }        
        public DateTimeOffset TimeStamp { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }

        public CodeFrequency() { }

        public CodeFrequency(DateTimeOffset ts, int adds, int dels)
        {
            TimeStamp = ts;
            Additions = adds;
            Deletions = dels;
        }
    }
}