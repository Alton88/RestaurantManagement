namespace RestaurantManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Convertednumberofguesttostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "NumberOfGuests", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "NumberOfGuests", c => c.Int(nullable: false));
        }
    }
}
