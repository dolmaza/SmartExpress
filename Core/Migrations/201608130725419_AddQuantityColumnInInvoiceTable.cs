namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuantityColumnInInvoiceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Quantity", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Quantity");
        }
    }
}
