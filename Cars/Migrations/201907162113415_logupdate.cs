namespace Cars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.logs", "carname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.logs", "carname");
        }
    }
}
