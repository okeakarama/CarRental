namespace Cars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customertelandemail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.logs", "customeremail", c => c.String());
            AddColumn("dbo.logs", "customertel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.logs", "customertel");
            DropColumn("dbo.logs", "customeremail");
        }
    }
}
