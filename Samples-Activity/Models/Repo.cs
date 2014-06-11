using Octokit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samples_Activity.Models
{
    public class Repo : Octokit.Repository
    {
        public IReadOnlyList<User> contributors { get; set; }
        public CommitActivity commitActivity { get; set; }
        public CodeFrequency codeFrequency { get; set; }
        public Participation participation { get; set; }
        public PunchCard punchCard { get; set; }

        public Repo(Repository repo, IReadOnlyList<User> contributors, CommitActivity commitActivity, 
            CodeFrequency codeFrequency, Participation participation, PunchCard punchCard)
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
            this.contributors = contributors;
            this.commitActivity = commitActivity;
            this.codeFrequency = codeFrequency;
            this.participation = participation;
            this.punchCard = punchCard;
        }
    }
}