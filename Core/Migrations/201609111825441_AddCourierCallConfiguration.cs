namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourierCallConfiguration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourierCalls", "CourierCallerTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.CourierCalls", "StatusID", c => c.Int(nullable: false));
            AlterColumn("dbo.CourierCalls", "MessageTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.CourierCalls", "TotalWeight", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.CourierCalls", "ServiceTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.CourierCalls", "PayerTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.CourierCalls", "CourierCallerTypeID");
            CreateIndex("dbo.CourierCalls", "StatusID");
            CreateIndex("dbo.CourierCalls", "MessageTypeID");
            CreateIndex("dbo.CourierCalls", "ServiceTypeID");
            CreateIndex("dbo.CourierCalls", "PayerTypeID");
            AddForeignKey("dbo.CourierCalls", "CourierCallerTypeID", "dbo.Dictionaries", "ID");
            AddForeignKey("dbo.CourierCalls", "MessageTypeID", "dbo.Dictionaries", "ID");
            AddForeignKey("dbo.CourierCalls", "PayerTypeID", "dbo.Dictionaries", "ID");
            AddForeignKey("dbo.CourierCalls", "ServiceTypeID", "dbo.Dictionaries", "ID");
            AddForeignKey("dbo.CourierCalls", "StatusID", "dbo.Dictionaries", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourierCalls", "StatusID", "dbo.Dictionaries");
            DropForeignKey("dbo.CourierCalls", "ServiceTypeID", "dbo.Dictionaries");
            DropForeignKey("dbo.CourierCalls", "PayerTypeID", "dbo.Dictionaries");
            DropForeignKey("dbo.CourierCalls", "MessageTypeID", "dbo.Dictionaries");
            DropForeignKey("dbo.CourierCalls", "CourierCallerTypeID", "dbo.Dictionaries");
            DropIndex("dbo.CourierCalls", new[] { "PayerTypeID" });
            DropIndex("dbo.CourierCalls", new[] { "ServiceTypeID" });
            DropIndex("dbo.CourierCalls", new[] { "MessageTypeID" });
            DropIndex("dbo.CourierCalls", new[] { "StatusID" });
            DropIndex("dbo.CourierCalls", new[] { "CourierCallerTypeID" });
            AlterColumn("dbo.CourierCalls", "PayerTypeID", c => c.Int());
            AlterColumn("dbo.CourierCalls", "ServiceTypeID", c => c.Int());
            AlterColumn("dbo.CourierCalls", "TotalWeight", c => c.String());
            AlterColumn("dbo.CourierCalls", "MessageTypeID", c => c.Int());
            AlterColumn("dbo.CourierCalls", "StatusID", c => c.Int());
            AlterColumn("dbo.CourierCalls", "CourierCallerTypeID", c => c.Int());
        }
    }
}
