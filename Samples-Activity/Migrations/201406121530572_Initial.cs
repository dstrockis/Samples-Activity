namespace Samples_Activity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Repoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        HtmlUrl = c.String(),
                        CloneUrl = c.String(),
                        GitUrl = c.String(),
                        SshUrl = c.String(),
                        SvnUrl = c.String(),
                        MirrorUrl = c.String(),
                        Name = c.String(),
                        FullName = c.String(),
                        Description = c.String(),
                        Homepage = c.String(),
                        Language = c.String(),
                        Private = c.Boolean(nullable: false),
                        Fork = c.Boolean(nullable: false),
                        ForksCount = c.Int(nullable: false),
                        WatchersCount = c.Int(nullable: false),
                        MasterBranch = c.String(),
                        OpenIssuesCount = c.Int(nullable: false),
                        PushedAt = c.DateTimeOffset(precision: 7),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        HasIssues = c.Boolean(nullable: false),
                        HasWiki = c.Boolean(nullable: false),
                        HasDownloads = c.Boolean(nullable: false),
                        Organization_Id = c.Int(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Organization_Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.Organization_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.CodeFrequencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTimeOffset(nullable: false, precision: 7),
                        Additions = c.Int(nullable: false),
                        Deletions = c.Int(nullable: false),
                        Repo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Repoes", t => t.Repo_Id)
                .Index(t => t.Repo_Id);
            
            CreateTable(
                "dbo.WeeklyCommitActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Week = c.Long(nullable: false),
                        Total = c.Int(nullable: false),
                        Repo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Repoes", t => t.Repo_Id)
                .Index(t => t.Repo_Id);
            
            CreateTable(
                "dbo.DayCounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        WeeklyCommitActivity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WeeklyCommitActivities", t => t.WeeklyCommitActivity_Id)
                .Index(t => t.WeeklyCommitActivity_Id);
            
            CreateTable(
                "dbo.Contributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorLogin = c.String(),
                        AuthorId = c.Int(nullable: false),
                        AuthorType = c.String(),
                        Total = c.Int(nullable: false),
                        Repo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Repoes", t => t.Repo_Id)
                .Index(t => t.Repo_Id);
            
            CreateTable(
                "dbo.WeeklyHashes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        W = c.Long(nullable: false),
                        A = c.Int(nullable: false),
                        D = c.Int(nullable: false),
                        C = c.Int(nullable: false),
                        Contributors_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contributors", t => t.Contributors_Id)
                .Index(t => t.Contributors_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GravatarId = c.String(),
                        SiteAdmin = c.Boolean(nullable: false),
                        AvatarUrl = c.String(),
                        Bio = c.String(),
                        Blog = c.String(),
                        Collaborators = c.Int(nullable: false),
                        Company = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        DiskUsage = c.Int(nullable: false),
                        Email = c.String(),
                        Followers = c.Int(nullable: false),
                        Following = c.Int(nullable: false),
                        Hireable = c.Boolean(nullable: false),
                        HtmlUrl = c.String(),
                        Location = c.String(),
                        Login = c.String(),
                        Name = c.String(),
                        OwnedPrivateRepos = c.Int(nullable: false),
                        PrivateGists = c.Int(nullable: false),
                        PublicGists = c.Int(nullable: false),
                        PublicRepos = c.Int(nullable: false),
                        TotalPrivateRepos = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Participations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        All = c.Int(nullable: false),
                        Owner = c.Int(nullable: false),
                        Repo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Repoes", t => t.Repo_Id)
                .Index(t => t.Repo_Id);
            
            CreateTable(
                "dbo.PunchCardPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                        HourOfTheDay = c.Int(nullable: false),
                        CommitCount = c.Int(nullable: false),
                        Repo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Repoes", t => t.Repo_Id)
                .Index(t => t.Repo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PunchCardPoints", "Repo_Id", "dbo.Repoes");
            DropForeignKey("dbo.Participations", "Repo_Id", "dbo.Repoes");
            DropForeignKey("dbo.Repoes", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Repoes", "Organization_Id", "dbo.Users");
            DropForeignKey("dbo.Contributors", "Repo_Id", "dbo.Repoes");
            DropForeignKey("dbo.WeeklyHashes", "Contributors_Id", "dbo.Contributors");
            DropForeignKey("dbo.WeeklyCommitActivities", "Repo_Id", "dbo.Repoes");
            DropForeignKey("dbo.DayCounts", "WeeklyCommitActivity_Id", "dbo.WeeklyCommitActivities");
            DropForeignKey("dbo.CodeFrequencies", "Repo_Id", "dbo.Repoes");
            DropIndex("dbo.PunchCardPoints", new[] { "Repo_Id" });
            DropIndex("dbo.Participations", new[] { "Repo_Id" });
            DropIndex("dbo.WeeklyHashes", new[] { "Contributors_Id" });
            DropIndex("dbo.Contributors", new[] { "Repo_Id" });
            DropIndex("dbo.DayCounts", new[] { "WeeklyCommitActivity_Id" });
            DropIndex("dbo.WeeklyCommitActivities", new[] { "Repo_Id" });
            DropIndex("dbo.CodeFrequencies", new[] { "Repo_Id" });
            DropIndex("dbo.Repoes", new[] { "Owner_Id" });
            DropIndex("dbo.Repoes", new[] { "Organization_Id" });
            DropTable("dbo.PunchCardPoints");
            DropTable("dbo.Participations");
            DropTable("dbo.Users");
            DropTable("dbo.WeeklyHashes");
            DropTable("dbo.Contributors");
            DropTable("dbo.DayCounts");
            DropTable("dbo.WeeklyCommitActivities");
            DropTable("dbo.CodeFrequencies");
            DropTable("dbo.Repoes");
        }
    }
}
