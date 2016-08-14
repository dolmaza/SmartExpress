namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedReceiverTelephoneNumberMaxLengthInInvoicesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "ReceiverTelephoneNumber", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "ReceiverTelephoneNumber", c => c.String(maxLength: 10));
        }
    }
}
