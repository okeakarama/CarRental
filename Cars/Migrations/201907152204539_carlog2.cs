namespace Cars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carlog2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.logs", "leasedate", c => c.String());
            AlterColumn("dbo.logs", "retudate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.logs", "retudate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.logs", "leasedate", c => c.DateTime(nullable: false));
        }
    }
}
