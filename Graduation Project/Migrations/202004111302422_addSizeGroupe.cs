namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSizeGroupe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SizeGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        producttypeId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producttypes", t => t.producttypeId, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => t.producttypeId)
                .Index(t => t.SizeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SizeGroups", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.SizeGroups", "producttypeId", "dbo.Producttypes");
            DropIndex("dbo.SizeGroups", new[] { "SizeId" });
            DropIndex("dbo.SizeGroups", new[] { "producttypeId" });
            DropTable("dbo.SizeGroups");
        }
    }
}
