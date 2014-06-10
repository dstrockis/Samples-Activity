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

        public DbSet<Repo> Repos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Repo>().HasKey(t => t.Id);
            //modelBuilder.Entity<Repo>().HasOptional(f => f.Source).WithRequired();
            modelBuilder.Entity<Repo>().Ignore(t => t.Parent);
            modelBuilder.Entity<Repo>().Ignore(t => t.Source);
        }

    }
}