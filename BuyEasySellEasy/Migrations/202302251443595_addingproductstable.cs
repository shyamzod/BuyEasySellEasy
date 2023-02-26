namespace BuyEasySellEasy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingproductstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Long(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Long(nullable: false),
                        Category = c.String(nullable: false),
                        Photo1 = c.String(),
                        Photo2 = c.String(),
                        Photo3 = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
