namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fresh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SizeGroup_Id", c => c.Int());
            CreateIndex("dbo.Products", "SizeGroup_Id");
            AddForeignKey("dbo.Products", "SizeGroup_Id", "dbo.SizeGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SizeGroup_Id", "dbo.SizeGroups");
            DropIndex("dbo.Products", new[] { "SizeGroup_Id" });
            DropColumn("dbo.Products", "SizeGroup_Id");
        }
    }
}
