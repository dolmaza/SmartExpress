namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedSenderTelephoneNumberMaxLengthInInvoicesTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "SenderTelephoneNumber", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "SenderTelephoneNumber", c => c.String());
        }
    }
}
