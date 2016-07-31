namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeIsVisibleColumnNullableInDictionaryTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dictionaries", "IsVisible", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dictionaries", "IsVisible", c => c.Boolean(nullable: false));
        }
    }
}
