namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class klh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Discount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Discount");
        }
    }
}
