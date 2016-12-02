namespace RestaurantManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedtoWaitingList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WaitingLists", "FirstName", c => c.String());
            AddColumn("dbo.WaitingLists", "LastName", c => c.String());
            AddColumn("dbo.WaitingLists", "DateAndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.WaitingLists", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WaitingLists", "Name", c => c.String());
            DropColumn("dbo.WaitingLists", "DateAndTime");
            DropColumn("dbo.WaitingLists", "LastName");
            DropColumn("dbo.WaitingLists", "FirstName");
        }
    }
}
