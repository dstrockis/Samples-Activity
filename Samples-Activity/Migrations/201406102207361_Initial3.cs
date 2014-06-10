namespace Samples_Activity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
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
                        Plan_Collaborators = c.Long(nullable: false),
                        Plan_Name = c.String(),
                        Plan_PrivateRepos = c.Long(nullable: false),
                        Plan_Space = c.Long(nullable: false),
                        Plan_BillingEmail = c.String(),
                        PrivateGists = c.Int(nullable: false),
                        PublicGists = c.Int(nullable: false),
                        PublicRepos = c.Int(nullable: false),
                        TotalPrivateRepos = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Repoes", "Organization_Id", c => c.Int());
            AddColumn("dbo.Repoes", "Owner_Id", c => c.Int());
            CreateIndex("dbo.Repoes", "Organization_Id");
            CreateIndex("dbo.Repoes", "Owner_Id");
            AddForeignKey("dbo.Repoes", "Organization_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Repoes", "Owner_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Repoes", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Repoes", "Organization_Id", "dbo.Users");
            DropIndex("dbo.Repoes", new[] { "Owner_Id" });
            DropIndex("dbo.Repoes", new[] { "Organization_Id" });
            DropColumn("dbo.Repoes", "Owner_Id");
            DropColumn("dbo.Repoes", "Organization_Id");
            DropTable("dbo.Users");
        }
    }
}
