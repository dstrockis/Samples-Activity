using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Samples_Activity.Models;
using System.Data.Entity;
using Octokit;

namespace Samples_Activity.DAL
{
    public class RepoContext : DbContext
    {
        public RepoContext()
            : base("DefaultConnection")
        {  }

        public DbSet<Repo> Repo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Repo>().HasKey(t => t.Id);
            modelBuilder.Entity<Repo>().Ignore(t => t.Parent);
            modelBuilder.Entity<Repo>().Ignore(t => t.Source);
            modelBuilder.Entity<User>().Ignore(t => t.Plan);

            modelBuilder.Entity<Repo>().HasMany(p => p.participation);
            modelBuilder.Entity<Repo>().HasMany(c => c.codeFrequency);
            modelBuilder.Entity<Repo>().HasMany(p => p.punchCard);
            modelBuilder.Entity<Repo>().HasMany(c => c.commitActivity);
            modelBuilder.Entity<Samples_Activity.Models.WeeklyCommitActivity>().HasMany(d => d.Days);
            modelBuilder.Entity<Repo>().HasMany(c => c.contributors);
            modelBuilder.Entity<Contributors>().HasMany(w => w.Weeks);
        }
    }
}