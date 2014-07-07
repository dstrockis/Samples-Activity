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
using System.Data;
using System.Data.Entity;
using System.Web.UI.DataVisualization.Charting;
using System.Web.Script.Serialization;
using Octokit.Helpers;
using System.Net.Mail;
using System.Net.Mime;


namespace Samples_Activity.Controllers
{
    public class HomeController : Controller
    {
        private RepoContext db = new RepoContext();
        public async Task<ActionResult> Index(string origin)
        {
            ViewBag.Origin = origin;

            var repoList = await db.Repo.ToListAsync();
            
            //Total Commits on AzureADSamples By Week -- Line Chart
            var weekHashes = db.Database.SqlQuery<long>(
                "Select h.W FROM WeeklyHashes h GROUP BY h.W"
                ).ToArray();
            var commitCounts = db.Database.SqlQuery<int>(
                "Select SUM(h.C) FROM WeeklyHashes h GROUP BY h.W"
                ).ToArray();
            var weeks = new string[weekHashes.Length];
            for (var i = 0; i < weekHashes.Length; i++)
            {
                weeks[i] = weekHashes[i].FromUnixTime().Date.Month.ToString() + '/' + weekHashes[i].FromUnixTime().Date.Day.ToString();
            }
            ViewData["weekArray"] = weeks;
            ViewData["commitArray"] = commitCounts;

            
            var repoNames = new string[repoList.Count];
            commitCounts = new int[repoList.Count];
            var forksCounts = new int[repoList.Count];
            var watchersCounts = new int[repoList.Count];
            for (int i=0; i < repoList.Count; i++)
            {

                //Commits Over Last X Weeks By Repo -- Radar Chart
                repoNames[i] = repoList[i].Name;
                int commitTotal = 0;
                var weekEnum1 = repoList[i].commitActivity.GetEnumerator();
                var weekEnum2 = repoList[i].commitActivity.GetEnumerator();
                for (int j = 0; j < Globals.numWeeksToInclude; j++)
                    weekEnum1.MoveNext();
                while (weekEnum1.MoveNext())
                    weekEnum2.MoveNext();
                while (weekEnum2.MoveNext())
                {
                    commitTotal += weekEnum2.Current.Total;
                }
                commitCounts[i] = commitTotal;

                //All-Time Forks and Watchers
                forksCounts[i] = repoList[i].ForksCount;
                watchersCounts[i] = repoList[i].WatchersCount;
            }
            ViewData["repoNames"] = repoNames;
            ViewData["repoMonthCommitCounts"] = commitCounts;
            ViewData["forksCounts"] = forksCounts;
            ViewData["watchersCounts"] = watchersCounts;


            return View();
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
            db.Database.ExecuteSqlCommand("delete from WeeklyHashes");
            db.Database.ExecuteSqlCommand("delete from DayCounts");
            db.Database.ExecuteSqlCommand("delete from WeeklyCommitActivities");
            db.Database.ExecuteSqlCommand("delete from PunchCardPoints");
            db.Database.ExecuteSqlCommand("delete from Participations");
            db.Database.ExecuteSqlCommand("delete from Contributors");
            db.Database.ExecuteSqlCommand("delete from CodeFrequencies");
            db.Database.ExecuteSqlCommand("delete from Repoes");
            db.Database.ExecuteSqlCommand("delete from Users");

            var github = new GitHubClient(new ProductHeaderValue(Globals.GitHubAppName))
            {
                Credentials = new Credentials("dstrockis", "password")
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
                    db.Repo.Add(newRepo);
                }
            }
            db.SaveChangesAsync();
            return RedirectToAction("Index", new { origin = "Refresh" });
            //return RedirectToRoute(new { controller = "Home", action = "About" });
        }

        public ActionResult SendMail(string origin)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(Globals.mailUser, Globals.mailUserPwd);
            client.EnableSsl = true;
            client.Credentials = credentials;

            var mail = new MailMessage("d.l.strockis@gmail.com", "d.l.strockis@gmail.com");
            mail.Subject = "Test Email";
            mail.Body = "<a href='https://http://aadsamplesactivity.azurewebsites.net/'>Click Here To View AAD Samples Github Data</a>";
            mail.IsBodyHtml = true;
            client.Send(mail);
            
            return RedirectToAction("Index", new { origin = origin });        
        }
    }
}