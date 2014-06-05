using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samples_Activity.Models
{
    public class RepoList
    {
        public List<string> repos { get; set; }
        public RepoList(string GitHubOrg)
        {
            repos = new List<string>();

            //var github = new GitHubClient(new ProductHeaderValue("AADSamplesActivity"));
            //var org = await github.Organization.Get(GitHubOrg);
            //var pubRepos = org.PublicRepos.

            var repoClient = new RepositoriesClient(new ApiConnection(new Connection(new ProductHeaderValue("AADSamplesActivity"))));
            var reposPagedCollection = repoClient.GetAllForOrg(GitHubOrg);
            var repoIEnumerator = reposPagedCollection.Result.GetEnumerator();

            int i=0;
            while (repoIEnumerator.MoveNext())
            {
                repos.Add(repoIEnumerator.Current.Name);
                i++;
            }
        }
    }
}