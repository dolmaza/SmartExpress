namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeParentIDColumnNullableInDictionaryTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Dictionaries", new[] { "ParentID" });
            AlterColumn("dbo.Dictionaries", "ParentID", c => c.Int());
            CreateIndex("dbo.Dictionaries", "ParentID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Dictionaries", new[] { "ParentID" });
            AlterColumn("dbo.Dictionaries", "ParentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Dictionaries", "ParentID");
        }
    }
}
