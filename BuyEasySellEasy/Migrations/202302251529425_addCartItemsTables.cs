namespace BuyEasySellEasy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCartItemsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Long(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ProductId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CartItemId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CartItems", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropIndex("dbo.CartItems", new[] { "UserId" });
            DropTable("dbo.CartItems");
        }
    }
}
