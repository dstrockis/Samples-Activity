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
        //public DbSet<Octokit.User> User { get; set; }
        //public DbSet<Samples_Activity.Models.CodeFrequency> CodeFrequency { get; set; }
        //public DbSet<Samples_Activity.Models.CommitActivity> CommitActivity { get; set; }
        //public DbSet<Samples_Activity.Models.ContributorSet> ContributorSet { get; set; }
        //public DbSet<Samples_Activity.Models.Participation> Participation { get; set; }
        //public DbSet<Samples_Activity.Models.PunchCard> PunchCard { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Repo>().HasKey(t => t.Id);
            //modelBuilder.ComplexType<Samples_Activity.Models.CodeFrequency>();
            //modelBuilder.ComplexType<Samples_Activity.Models.WeeklyCommitActivity>();
            //modelBuilder.ComplexType<Samples_Activity.Models.Contributors>();
            //modelBuilder.ComplexType<Samples_Activity.Models.Participation>();
            //modelBuilder.ComplexType<Samples_Activity.Models.PunchCardPoint>();
            modelBuilder.Entity<Repo>().Ignore(t => t.Parent);
            modelBuilder.Entity<Repo>().Ignore(t => t.Source);
            modelBuilder.Entity<User>().Ignore(t => t.Plan);

            //modelBuilder.Entity<Samples_Activity.Models.CodeFrequency>().HasRequired(r => r.repo).WithRequiredDependent(c => c.codeFrequency);
            //modelBuilder.Entity<Samples_Activity.Models.CommitActivity>().HasRequired(r => r.repo).WithRequiredDependent(c => c.commitActivity);
            //modelBuilder.Entity<Samples_Activity.Models.ContributorSet>().HasRequired(r => r.repo).WithRequiredDependent(c => c.contributorSet);
            //modelBuilder.Entity<Samples_Activity.Models.Participation>().HasRequired(r => r.repo).WithRequiredDependent(p => p.participation);
            //modelBuilder.Entity<Samples_Activity.Models.PunchCard>().HasRequired(r => r.repo).WithRequiredDependent(p => p.punchCard);

            modelBuilder.Entity<Repo>().HasMany(p => p.participation);
            modelBuilder.Entity<Repo>().HasMany(c => c.codeFrequency);
            modelBuilder.Entity<Repo>().HasMany(p => p.punchCard);
            modelBuilder.Entity<Samples_Activity.Models.Participation>().HasMany(o => o.Owner);
            modelBuilder.Entity<Samples_Activity.Models.Participation>().HasMany(a => a.All);
            modelBuilder.Entity<Repo>().HasMany(c => c.commitActivity);
            modelBuilder.Entity<Samples_Activity.Models.WeeklyCommitActivity>().HasMany(d => d.Days);
            modelBuilder.Entity<Repo>().HasMany(c => c.contributors);
            modelBuilder.Entity<Contributors>().HasMany(w => w.Weeks);

            //modelBuilder.Entity<Repo>().HasOptional(t => t.commitActivity).WithRequired();
        }

    }
}