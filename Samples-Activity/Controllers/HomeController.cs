﻿using DotNetOpenAuth.OAuth2;
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
using System.Data;
using System.Data.Entity;
using System.Web.UI.DataVisualization.Charting;



namespace Samples_Activity.Controllers
{
    public class HomeController : Controller
    {
        private RepoContext db = new RepoContext();
        public async Task<ActionResult> Index()
        {
            var repoList = await db.Repo.ToListAsync();
            var weeks = db.Database.SqlQuery<long>(
                "Select h.W FROM WeeklyHashes h GROUP BY h.W"
                ).ToArray();
            var commitCounts = db.Database.SqlQuery<int>(
                "Select SUM(h.C) FROM WeeklyHashes h GROUP BY h.W"
                ).ToArray();
            var model = new { weekArray = weeks, commitArray = commitCounts }; //BOOKMARK
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

        public async Task<ActionResult> Refresh()
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
            return RedirectToAction("Index");
            //return RedirectToRoute(new { controller = "Home", action = "About" });
        }
    }
}