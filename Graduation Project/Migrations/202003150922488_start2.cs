namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "Customer_Id" });
            RenameColumn(table: "dbo.Orders", name: "Products_Id", newName: "Product_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_Products_Id", newName: "IX_Product_Id");
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Qty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Orders", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Products", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Customer_Id", c => c.Int());
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropColumn("dbo.Orders", "DateTime");
            DropTable("dbo.OrderItems");
            RenameIndex(table: "dbo.Orders", name: "IX_Product_Id", newName: "IX_Products_Id");
            RenameColumn(table: "dbo.Orders", name: "Product_Id", newName: "Products_Id");
            CreateIndex("dbo.Products", "Customer_Id");
            AddForeignKey("dbo.Products", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
