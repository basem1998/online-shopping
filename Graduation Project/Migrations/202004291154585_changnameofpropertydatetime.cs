namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changnameofpropertydatetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "OrderDate");
        }
    }
}
