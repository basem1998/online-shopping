namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editchangebetweenmodlesizeandsizegroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sizes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "SizeGroup_Id", "dbo.SizeGroups");
            DropForeignKey("dbo.SizeGroups", "SizeId", "dbo.Sizes");
            DropIndex("dbo.Products", new[] { "SizeGroup_Id" });
            DropIndex("dbo.Sizes", new[] { "ProductId" });
            DropIndex("dbo.SizeGroups", new[] { "SizeId" });
            AddColumn("dbo.Sizes", "SizegroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sizes", "SizegroupId");
            AddForeignKey("dbo.Sizes", "SizegroupId", "dbo.SizeGroups", "Id", cascadeDelete: false);
            DropColumn("dbo.Products", "SizeGroup_Id");
            DropColumn("dbo.Sizes", "ProductId");
            DropColumn("dbo.SizeGroups", "SizeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SizeGroups", "SizeId", c => c.Int(nullable: false));
            AddColumn("dbo.Sizes", "ProductId", c => c.Guid(nullable: false));
            AddColumn("dbo.Products", "SizeGroup_Id", c => c.Int());
            DropForeignKey("dbo.Sizes", "SizegroupId", "dbo.SizeGroups");
            DropIndex("dbo.Sizes", new[] { "SizegroupId" });
            DropColumn("dbo.Sizes", "SizegroupId");
            CreateIndex("dbo.SizeGroups", "SizeId");
            CreateIndex("dbo.Sizes", "ProductId");
            CreateIndex("dbo.Products", "SizeGroup_Id");
            AddForeignKey("dbo.SizeGroups", "SizeId", "dbo.Sizes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "SizeGroup_Id", "dbo.SizeGroups", "Id");
            AddForeignKey("dbo.Sizes", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
