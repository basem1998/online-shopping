namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editproductIdtoguid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sizes", "Product_Id", "dbo.Products");
            DropIndex("dbo.Sizes", new[] { "Product_Id" });
            DropColumn("dbo.Sizes", "ProductId");
            RenameColumn(table: "dbo.Sizes", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.Sizes", "ProductId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Sizes", "ProductId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Sizes", "ProductId");
            AddForeignKey("dbo.Sizes", "ProductId", "dbo.Products", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sizes", "ProductId", "dbo.Products");
            DropIndex("dbo.Sizes", new[] { "ProductId" });
            AlterColumn("dbo.Sizes", "ProductId", c => c.Guid());
            AlterColumn("dbo.Sizes", "ProductId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Sizes", name: "ProductId", newName: "Product_Id");
            AddColumn("dbo.Sizes", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sizes", "Product_Id");
            AddForeignKey("dbo.Sizes", "Product_Id", "dbo.Products", "Id");
        }
    }
}
