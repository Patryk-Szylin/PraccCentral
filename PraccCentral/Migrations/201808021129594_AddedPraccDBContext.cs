namespace PraccCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPraccDBContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Praccs",
                c => new
                    {
                        PraccId = c.Int(nullable: false, identity: true),
                        Map = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PraccId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Praccs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Praccs", new[] { "User_Id" });
            DropTable("dbo.Praccs");
        }
    }
}
