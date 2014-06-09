using Microsoft.Owin.Logging;
using Octokit;
using Octokit.Internal;
using Owin.Security.Providers.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Samples_Activity.Models
{
    public class RepoList
    {
        public struct repo
        {
            public Octokit.Repository _repo;
            public int contributorCount;
            private IReadOnlyList<Contributor> contributors;
            private IReadOnlyList<CommitActivity> commitActivity;
            private IReadOnlyList<CodeFrequency> codeFrequency;
            private IReadOnlyList<Participation> participation;
            private IReadOnlyList<PunchCard> punchCard;

            public repo(Octokit.Repository repo, int contributorCount, IReadOnlyList<Contributor> contributors, IReadOnlyList<CommitActivity> commitActivity,
                IReadOnlyList<CodeFrequency> codeFrequency, IReadOnlyList<Participation> participation, IReadOnlyList<PunchCard> punchCard)
            { 
              this._repo = repo;
              this.contributorCount = contributorCount;
              this.contributors = contributors;
              this.commitActivity = commitActivity;
              this.codeFrequency = codeFrequency;
              this.participation = participation;
              this.punchCard = punchCard;
            }
        }
        public List<repo> repos { get; set; }
        public RepoList(string GitHubOrg, string GitHubAppName)
        {
            //Initialize List of Repos for Organization
            repos = new List<repo>();

            //Get RepositoriesClient
            var apiConnection = new ApiConnection(new Connection(new Octokit.ProductHeaderValue(GitHubAppName), new InMemoryCredentialStore(new Credentials("dstrockis", "@Mammoth2")))); //ProductHeaderValue Is Ambiguous //Using hardcoded uname/pword
            var repoClient = new RepositoriesClient(apiConnection);

            //Get Repos for Organization
            var reposPromise = repoClient.GetAllForOrg(GitHubOrg);

            //Add Repos to List
            foreach (var repo in reposPromise.Result)
            {
                try
                {
                    var contributorCount = apiConnection.GetAll<Octokit.Contributor>(new Uri(Globals.GitHubUriBase + "/repos/" + repo.Owner.Login + "/" + repo.Name + "/stats/contributors")).Result.Count;
                    var temp = apiConnection.GetAll<Octokit.CommitActivity>(new Uri(Globals.GitHubUriBase + "/repos/" + repo.Owner.Login + "/" + repo.Name + "/stats/commit_activity")).Result;
                    repos.Add(new repo(repo, contributorCount, null, null, null, null, null));
                }
                catch { Exception e; };
            } 
        }
    }
}