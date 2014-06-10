namespace Samples_Activity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Repositories",
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
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Repoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Repoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contributorCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropTable("dbo.Repositories");
        }
    }
}
