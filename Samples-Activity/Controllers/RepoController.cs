using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Samples_Activity.DAL;
using Samples_Activity.Models;

namespace Samples_Activity.Controllers
{
    public class RepoController : Controller
    {
        private RepoContext db = new RepoContext();

        // GET: Repo
        public async Task<ActionResult> Index()
        {
            return View(await db.Repo.ToListAsync());
        }

        // GET: Repo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repo repo = await db.Repo.FindAsync(id);
            if (repo == null)
            {
                return HttpNotFound();
            }
            return View(repo);
        }

        // GET: Repo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Url,HtmlUrl,CloneUrl,GitUrl,SshUrl,SvnUrl,MirrorUrl,Name,FullName,Description,Homepage,Language,Private,Fork,ForksCount,WatchersCount,MasterBranch,OpenIssuesCount,PushedAt,CreatedAt,UpdatedAt,HasIssues,HasWiki,HasDownloads")] Repo repo)
        {
            if (ModelState.IsValid)
            {
                db.Repo.Add(repo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(repo);
        }

        // GET: Repo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repo repo = await db.Repo.FindAsync(id);
            if (repo == null)
            {
                return HttpNotFound();
            }
            return View(repo);
        }

        // POST: Repo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Url,HtmlUrl,CloneUrl,GitUrl,SshUrl,SvnUrl,MirrorUrl,Name,FullName,Description,Homepage,Language,Private,Fork,ForksCount,WatchersCount,MasterBranch,OpenIssuesCount,PushedAt,CreatedAt,UpdatedAt,HasIssues,HasWiki,HasDownloads")] Repo repo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(repo);
        }

        // GET: Repo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repo repo = await db.Repo.FindAsync(id);
            if (repo == null)
            {
                return HttpNotFound();
            }
            return View(repo);
        }

        // POST: Repo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Repo repo = await db.Repo.FindAsync(id);
            db.Repo.Remove(repo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
