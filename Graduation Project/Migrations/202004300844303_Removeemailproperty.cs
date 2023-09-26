namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removeemailproperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Email", c => c.String());
        }
    }
}
