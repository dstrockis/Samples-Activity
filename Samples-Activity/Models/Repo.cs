using Octokit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samples_Activity.Models
{
    public class Repo : Octokit.Repository
    {
        public virtual ICollection<CodeFrequency> codeFrequency { get; set; }
        public virtual ICollection<WeeklyCommitActivity> commitActivity { get; set; }
        public virtual ICollection<Contributors> contributors { get; set; }
        public virtual ICollection<Participation> participation { get; set; }
        public virtual ICollection<PunchCardPoint> punchCard { get; set; }

        public Repo() {}

        public Repo(Octokit.Repository repo, IEnumerable<Contributor> contributors, Octokit.CommitActivity commitActivity,
            Octokit.CodeFrequency codeFrequency, Octokit.Participation participation, Octokit.PunchCard punchCard)
        {
            this.CloneUrl = repo.CloneUrl;
            this.CreatedAt = repo.CreatedAt;
            this.Description = repo.Description;
            this.Fork = repo.Fork;
            this.FullName = repo.FullName;
            this.GitUrl = repo.GitUrl;
            this.HasDownloads = repo.HasDownloads;
            this.HasIssues = repo.HasIssues;
            this.HasWiki = repo.HasWiki;
            this.Homepage = repo.Homepage;
            this.HtmlUrl = repo.HtmlUrl;
            this.Id = repo.Id;
            this.Language = repo.Language;
            this.MasterBranch = repo.MasterBranch;
            this.MirrorUrl = repo.MirrorUrl;
            this.Name = repo.Name;
            this.OpenIssuesCount = repo.OpenIssuesCount;
            this.Organization = repo.Organization;
            this.Owner = repo.Owner;
            this.Parent = repo.Parent;
            this.Private = repo.Private;
            this.PushedAt = repo.PushedAt;
            this.Source = repo.Source;
            this.SshUrl = repo.SshUrl;
            this.SvnUrl = repo.SvnUrl;
            this.UpdatedAt = repo.UpdatedAt;
            this.Url = repo.Url;

            this.ForksCount = repo.ForksCount;
            this.WatchersCount = repo.WatchersCount;

            this.contributors = new List<Contributors>();
            foreach (var contributor in contributors)
            {
                this.contributors.Add(new Contributors(contributor.Author, contributor.Total, contributor.Weeks));
            }
            this.commitActivity = new List<WeeklyCommitActivity>();
            foreach (var weeklyAct in commitActivity.Activity)
            {
                this.commitActivity.Add(new WeeklyCommitActivity(weeklyAct));
            }
            this.codeFrequency = new List<CodeFrequency>();
            foreach (var AAndDByWeek in codeFrequency.AdditionsAndDeletionsByWeek)
            {
                this.codeFrequency.Add(new CodeFrequency(AAndDByWeek.Timestamp, AAndDByWeek.Additions, AAndDByWeek.Deletions));
            }
            this.participation = new List<Participation>();
            var AllEnum = participation.All.GetEnumerator();
            var OwnEnum = participation.Owner.GetEnumerator();
            while (AllEnum.MoveNext() && OwnEnum.MoveNext())
            {
                this.participation.Add(new Participation(AllEnum.Current, OwnEnum.Current));
            }
            this.punchCard = new List<PunchCardPoint>();
            foreach (var pcp in punchCard.PunchPoints)
            {
                this.punchCard.Add(new PunchCardPoint(pcp.DayOfWeek, pcp.HourOfTheDay, pcp.CommitCount));
            }
        }
    }
}