using DotNetOpenAuth.OAuth2;
using Octokit;
using Octokit.Internal;
using Samples_Activity.DAL;
using Samples_Activity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace Samples_Activity.Controllers
{
    public class HomeController : Controller
    {
        private RepoContext db = new RepoContext();
        public async Task<ActionResult> Index()
        {   
            var github = new GitHubClient(new ProductHeaderValue(Globals.GitHubAppName))
            {
                Credentials = new Credentials("dstrockis", "@Mammoth2")
            };
            var repos = await github.Repository.GetAllForOrg(Globals.GitHubOrg);

            var model = new List<Repo>();
            foreach (var repo in repos) 
            {
                var contributors = await github.Repository.Statistics.GetContributors(repo.Owner.Login, repo.Name);
                var commitActivity = await github.Repository.Statistics.GetCommitActivity(repo.Owner.Login, repo.Name);
                var codeFrequency = await github.Repository.Statistics.GetCodeFrequency(repo.Owner.Login, repo.Name);
                var participation = await github.Repository.Statistics.GetParticipation(repo.Owner.Login, repo.Name);
                var punchCard = await github.Repository.Statistics.GetPunchCard(repo.Owner.Login, repo.Name);

                var newRepo = new Repo(repo, contributors, commitActivity, codeFrequency, participation, punchCard);
                model.Add(newRepo);
                if (ModelState.IsValid)
                {
                    db.Repo.AddOrUpdate(newRepo);
                    db.SaveChanges();
                    //db.SaveChangesAsync();
                }
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}