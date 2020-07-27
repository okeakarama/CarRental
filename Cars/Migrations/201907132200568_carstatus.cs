namespace Cars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.cars", "status", c => c.String());
            DropColumn("dbo.cars", "isavailable");
            DropColumn("dbo.cars", "isunavailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.cars", "isunavailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.cars", "isavailable", c => c.Boolean(nullable: false));
            DropColumn("dbo.cars", "status");
        }
    }
}
