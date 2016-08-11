namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTelephoneNumberMaxLengthTo20InUsersTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "TelephoneNumber", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "TelephoneNumber", c => c.String(maxLength: 10));
        }
    }
}
