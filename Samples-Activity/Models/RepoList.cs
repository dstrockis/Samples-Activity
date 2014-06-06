using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Samples_Activity.Models
{
    public class RepoList
    {
        public struct repo
        {
            public Octokit.Repository _repo;
            public int _contributorCount;

            public repo(Octokit.Repository repo, int contributorCount)
            { 
              this._repo = repo;
              this._contributorCount = contributorCount;
            }
        }
        public List<repo> repos { get; set; }
        public RepoList(string GitHubOrg, string GitHubAppName)
        {
            ////Initialize List of Repos for Organization
            //repos = new List<repo>();

            ////Get RepositoriesClient
            //var apiConnection = new ApiConnection(new Connection(new ProductHeaderValue(GitHubAppName)));
            //var repoClient = new RepositoriesClient(apiConnection);
            
            ////Get Repos for Organization
            //var reposPromise = repoClient.GetAllForOrg(GitHubOrg);
            
            ////Add Repos to List
            //foreach (var repo in reposPromise.Result)
            //{
            //    var contributorCount = apiConnection.GetAll<Octokit.Contributor>(new Uri(Globals.GitHubUriBase + "/repos/" + repo.Owner.Login + "/" + repo.Name + "/stats/contributors")).Result.Count;
            //    repos.Add(new repo(repo, contributorCount));
            //} 
        }
    }
}