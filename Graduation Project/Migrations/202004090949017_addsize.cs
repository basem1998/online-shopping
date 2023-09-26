namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductId = c.Int(nullable: false),
                        Product_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.OrderItems", "SizeId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderItems", "SizeId");
            AddForeignKey("dbo.OrderItems", "SizeId", "dbo.Sizes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sizes", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "SizeId", "dbo.Sizes");
            DropIndex("dbo.Sizes", new[] { "Product_Id" });
            DropIndex("dbo.OrderItems", new[] { "SizeId" });
            DropColumn("dbo.OrderItems", "SizeId");
            DropTable("dbo.Sizes");
        }
    }
}
