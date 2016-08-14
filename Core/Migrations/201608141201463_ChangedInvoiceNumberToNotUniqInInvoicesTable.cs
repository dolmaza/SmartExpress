namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedInvoiceNumberToNotUniqInInvoicesTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Invoices", "IX_InvoiceNumberUniq");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Invoices", "InvoiceNumber", unique: true, name: "IX_InvoiceNumberUniq");
        }
    }
}
