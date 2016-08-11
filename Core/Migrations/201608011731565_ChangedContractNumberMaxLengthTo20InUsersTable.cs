namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedContractNumberMaxLengthTo20InUsersTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", "IX_ContractNumberUniq");
            AlterColumn("dbo.Users", "ContractNumber", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Users", "ContractNumber", unique: true, name: "IX_ContractNumberUniq");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "IX_ContractNumberUniq");
            AlterColumn("dbo.Users", "ContractNumber", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.Users", "ContractNumber", unique: true, name: "IX_ContractNumberUniq");
        }
    }
}
