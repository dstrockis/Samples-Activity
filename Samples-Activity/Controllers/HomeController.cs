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
            var contributors = await github.Repository.GetAllContributors(repos[0].Owner.Login, repos[0].Name);
            var commitActivity = await github.Repository.Statistics.GetCommitActivity(repos[0].Owner.Login, repos[0].Name);
            var codeFrequency = await github.Repository.Statistics.GetCodeFrequency(repos[0].Owner.Login, repos[0].Name);
            var participation = await github.Repository.Statistics.GetParticipation(repos[0].Owner.Login, repos[0].Name);
            var punchCard = await github.Repository.Statistics.GetPunchCard(repos[0].Owner.Login, repos[0].Name);

            //var model = new Repo(repos[0], contributors.Count, contributors, commitActivity, codeFrequency, participation, punchCard);
            var model = new Repo(repos[0]);

            if (ModelState.IsValid)
            {
                db.Repos.Add(model);
                //db.SaveChanges();
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