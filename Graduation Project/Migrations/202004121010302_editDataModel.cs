namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDataModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "size");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "size", c => c.String());
            DropColumn("dbo.OrderItems", "Price");
        }
    }
}
