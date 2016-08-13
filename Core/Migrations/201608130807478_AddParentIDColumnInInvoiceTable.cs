namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParentIDColumnInInvoiceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "ParentID", c => c.Int());
            CreateIndex("dbo.Invoices", "ParentID");
            AddForeignKey("dbo.Invoices", "ParentID", "dbo.Invoices", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "ParentID", "dbo.Invoices");
            DropIndex("dbo.Invoices", new[] { "ParentID" });
            DropColumn("dbo.Invoices", "ParentID");
        }
    }
}
