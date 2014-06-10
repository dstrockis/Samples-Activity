namespace Samples_Activity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Repositories", newName: "Repoes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Repoes", newName: "Repositories");
        }
    }
}
