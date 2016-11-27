namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourierCallTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourierCalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourierCallerTypeID = c.Int(),
                        StatusID = c.Int(),
                        CourierCallerContractNubmer = c.String(),
                        CourierCallerPersonalNumber = c.String(),
                        CourierCallerCompanyName = c.String(),
                        CourierCallerFirstname = c.String(),
                        CourierCallerLastname = c.String(),
                        CourierCallerMobileNumber = c.String(),
                        CourierCallerEmail = c.String(),
                        SenderAddress = c.String(),
                        ReceiverAddress = c.String(),
                        MessageTypeID = c.String(),
                        MessageQuantity = c.String(),
                        TotalWeight = c.String(),
                        ServiceTypeID = c.String(),
                        PayerTypeID = c.String(),
                        PayerContractNumber = c.String(),
                        PayerAddress = c.String(),
                        PayerCompanyName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CourierCalls");
        }
    }
}
