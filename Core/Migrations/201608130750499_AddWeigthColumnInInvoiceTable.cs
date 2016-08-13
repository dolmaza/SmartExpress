namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeigthColumnInInvoiceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Weigth", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Weigth");
        }
    }
}
