namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedRelationtipsInInvoiceTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Invoices", new[] { "MessageTypeID" });
            DropIndex("dbo.Invoices", new[] { "MessageModeID" });
            DropIndex("dbo.Invoices", new[] { "PayerID" });
            DropIndex("dbo.Invoices", new[] { "FormOfPaymentID" });
            DropIndex("dbo.Invoices", new[] { "UserID" });
            AlterColumn("dbo.Invoices", "MessageTypeID", c => c.Int());
            AlterColumn("dbo.Invoices", "MessageModeID", c => c.Int());
            AlterColumn("dbo.Invoices", "PayerID", c => c.Int());
            AlterColumn("dbo.Invoices", "FormOfPaymentID", c => c.Int());
            AlterColumn("dbo.Invoices", "UserID", c => c.Int());
            CreateIndex("dbo.Invoices", "MessageTypeID");
            CreateIndex("dbo.Invoices", "MessageModeID");
            CreateIndex("dbo.Invoices", "PayerID");
            CreateIndex("dbo.Invoices", "FormOfPaymentID");
            CreateIndex("dbo.Invoices", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Invoices", new[] { "UserID" });
            DropIndex("dbo.Invoices", new[] { "FormOfPaymentID" });
            DropIndex("dbo.Invoices", new[] { "PayerID" });
            DropIndex("dbo.Invoices", new[] { "MessageModeID" });
            DropIndex("dbo.Invoices", new[] { "MessageTypeID" });
            AlterColumn("dbo.Invoices", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "FormOfPaymentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "PayerID", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "MessageModeID", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "MessageTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "UserID");
            CreateIndex("dbo.Invoices", "FormOfPaymentID");
            CreateIndex("dbo.Invoices", "PayerID");
            CreateIndex("dbo.Invoices", "MessageModeID");
            CreateIndex("dbo.Invoices", "MessageTypeID");
        }
    }
}
