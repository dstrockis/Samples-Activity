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
                        id = c.Int(nullable: false, identity: true),
                        contributorCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Repoes");
        }
    }
}
