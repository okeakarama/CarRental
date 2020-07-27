namespace Cars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class main : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cars",
                c => new
                    {
                        carid = c.Int(nullable: false, identity: true),
                        carname = c.String(),
                        carimage = c.String(),
                        price = c.String(),
                        isavailable = c.Boolean(nullable: false),
                        isunavailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.carid);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfile");
            DropTable("dbo.cars");
        }
    }
}
