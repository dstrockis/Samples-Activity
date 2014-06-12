using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samples_Activity.Models
{
    public class Contributors
    {
        public class WeeklyHash
        {
            public int Id { get; set; }
            public long W { get; set; }
            public int A { get; set; }
            public int D { get; set; }
            public int C { get; set; }

            public WeeklyHash() { }
            public WeeklyHash(long W, int A, int D, int C)
            {
                this.W = W;
                this.A = A;
                this.D = D;
                this.C = C;
            }
        }
        public int Id { get; set; }
        public string AuthorLogin { get; set; }
        public int AuthorId { get; set; }
        public string AuthorType { get; set; }
        public int Total { get; set; }
        public ICollection<WeeklyHash> Weeks { get; set; }

        public Contributors() { }

        public Contributors(Octokit.Author author, int total, IEnumerable<Octokit.WeeklyHash> weeks)
        {
            AuthorLogin = author.Login;
            AuthorId = author.Id;
            AuthorType = author.Type;
            Total = total;
            Weeks = new List<WeeklyHash>();
            foreach (var week in weeks)
            {
                Weeks.Add(new WeeklyHash(week.W, week.A, week.D, week.C));
            }
        }
    }
}