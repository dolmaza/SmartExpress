namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourierCallTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourierCalls", "MessageTypeID", c => c.Int());
            AlterColumn("dbo.CourierCalls", "ServiceTypeID", c => c.Int());
            AlterColumn("dbo.CourierCalls", "PayerTypeID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourierCalls", "PayerTypeID", c => c.String());
            AlterColumn("dbo.CourierCalls", "ServiceTypeID", c => c.String());
            AlterColumn("dbo.CourierCalls", "MessageTypeID", c => c.String());
        }
    }
}
