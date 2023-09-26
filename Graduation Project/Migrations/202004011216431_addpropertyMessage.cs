namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpropertyMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "Message");
        }
    }
}
