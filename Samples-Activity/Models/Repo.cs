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

        public Repo(Octokit.Repository repo, IReadOnlyList<Contributor> contributors, Octokit.CommitActivity commitActivity,
            Octokit.CodeFrequency codeFrequency, Octokit.Participation participation, Octokit.PunchCard punchCard)
        {
            this.CloneUrl = repo.CloneUrl;
            this.CreatedAt = repo.CreatedAt;
            this.Description = repo.Description;
            this.Fork = repo.Fork;
            this.ForksCount = repo.ForksCount;
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
            this.WatchersCount = repo.WatchersCount;

            this.contributors = new List<Contributors>();
            foreach (var contributor in contributors)
            {
                this.contributors.Add(new Contributors(contributor.Author, contributor.Total, contributor.Weeks));
            }
            foreach (var weeklyAct in commitActivity.Activity)
            {
                this.commitActivity.Add(new WeeklyCommitActivity(weeklyAct));
            }

            //this.commitActivity = new CommitActivity(commitActivity, this);
            //this.codeFrequency = new CodeFrequency(codeFrequency, this);
            //this.participation = new Participation(participation, this);
            //this.punchCard = new PunchCard(punchCard, this);
        }
    }
}