namespace Cars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class log : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.logs",
                c => new
                    {
                        logid = c.Int(nullable: false, identity: true),
                        customername = c.String(),
                        leasedate = c.DateTime(nullable: false),
                        retudate = c.DateTime(nullable: false),
                        carid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.logid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.logs");
        }
    }
}
