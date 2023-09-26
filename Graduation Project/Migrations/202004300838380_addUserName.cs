namespace Graduation_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "UserName");
        }
    }
}
