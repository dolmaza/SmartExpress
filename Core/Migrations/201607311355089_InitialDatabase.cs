namespace Core.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dictionaries",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ParentID = c.Int(nullable: false),
                    Caption = c.String(nullable: false, maxLength: 200),
                    StringCode = c.String(maxLength: 500),
                    IntCode = c.Int(),
                    Level = c.Int(),
                    Hierarchy = c.String(),
                    DictionaryCode = c.Int(nullable: false),
                    IsVisible = c.Boolean(nullable: false),
                    SortVal = c.Int(),
                    CreateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dictionaries", t => t.ParentID)
                .Index(t => t.ParentID);

            Sql("CREATE  TRIGGER [dbo].[OnDictionariesHierarchyCalc] ON [dbo].[Dictionaries] FOR INSERT, UPDATE AS DECLARE @DEL bit = 0 DECLARE @INS bit  = 0 DECLARE @numrows int = @@rowcount DECLARE @Delimiter varchar(1) = '.' IF EXISTS (SELECT TOP 1 1 FROM DELETED) SET @DEL=1 IF EXISTS (SELECT TOP 1 1 FROM INSERTED) SET @INS = 1  IF @INS = 0 AND @DEL = 0 RETURN IF @INS = 1 AND @DEL = 0 BEGIN IF @numrows > 1 BEGIN RAISERROR('Only single row inserts are supported!', 16, 1) ROLLBACK TRAN RETURN END ELSE BEGIN UPDATE D SET D.[Level] = CASE WHEN D.ParentID IS NULL THEN 0 ELSE D1.[Level] + 1 END, D.Hierarchy = CASE WHEN D.ParentID IS NULL THEN @Delimiter ELSE D1.Hierarchy END + CAST(D.ID AS varchar(10)) + @Delimiter FROM Dictionaries D INNER JOIN inserted I ON I.ID = D.ID LEFT JOIN Dictionaries D1 ON D.ParentID = D1.ID END END ELSE BEGIN IF UPDATE(ID) BEGIN RAISERROR('Updates to codeid not allowed!', 16, 1) ROLLBACK TRAN RETURN END ELSE BEGIN IF UPDATE(ParentID) or UPDATE(StringCode) BEGIN UPDATE D SET D.[Level] = D.[Level] - I.[Level] + CASE WHEN I.ParentID IS NULL THEN 0 ELSE D1.[Level] + 1 END, D.Hierarchy = ISNULL(D1.Hierarchy, '.') + CAST(I.ID AS varchar(10)) + '.' + ISNULL(RIGHT(D.Hierarchy, LEN(D.Hierarchy) - LEN(I.Hierarchy)), '') FROM Dictionaries D INNER JOIN INSERTED I ON D.Hierarchy LIKE I.Hierarchy + '%' LEFT JOIN Dictionaries D1 ON I.ParentID = D1.ID END END END");

            CreateTable(
                "dbo.Invoices",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    InvoiceNumber = c.String(maxLength: 30),
                    MessageTypeID = c.Int(nullable: false),
                    ReceiveDate = c.DateTime(),
                    DeliveryDate = c.DateTime(),
                    UnitPrice = c.Decimal(precision: 18, scale: 2),
                    TotalPrice = c.Decimal(precision: 18, scale: 2),
                    Direction = c.String(maxLength: 500),
                    MessageModeID = c.Int(nullable: false),
                    PayerID = c.Int(nullable: false),
                    FormOfPaymentID = c.Int(nullable: false),
                    UserID = c.Int(nullable: false),
                    CompanyName = c.String(maxLength: 500),
                    SenderFirstname = c.String(maxLength: 500),
                    SenderLastname = c.String(maxLength: 500),
                    SenderTelephoneNumber = c.String(),
                    SenderAddress = c.String(maxLength: 500),
                    ReceiverFirstname = c.String(maxLength: 500),
                    ReceiverLastname = c.String(maxLength: 500),
                    ReceiverTelephoneNumber = c.String(maxLength: 10),
                    ReceiverAddress = c.String(maxLength: 500),
                    WhoReceived = c.String(maxLength: 500),
                    WhoReceivedAdditional = c.String(),
                    CreateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dictionaries", t => t.FormOfPaymentID)
                .ForeignKey("dbo.Dictionaries", t => t.MessageModeID)
                .ForeignKey("dbo.Dictionaries", t => t.MessageTypeID)
                .ForeignKey("dbo.Dictionaries", t => t.PayerID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.InvoiceNumber, unique: true, name: "IX_InvoiceNumberUniq")
                .Index(t => t.MessageTypeID)
                .Index(t => t.MessageModeID)
                .Index(t => t.PayerID)
                .Index(t => t.FormOfPaymentID)
                .Index(t => t.UserID);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ContractNumber = c.String(nullable: false, maxLength: 10),
                    IDNumber = c.String(maxLength: 20),
                    Firstname = c.String(maxLength: 500),
                    Lastname = c.String(maxLength: 500),
                    Address = c.String(maxLength: 500),
                    TelephoneNumber = c.String(maxLength: 10),
                    CompanyName = c.String(maxLength: 500),
                    RoleID = c.Int(nullable: false),
                    CreateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dictionaries", t => t.RoleID)
                .Index(t => t.ContractNumber, unique: true, name: "IX_ContractNumberUniq")
                .Index(t => t.RoleID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Dictionaries", "ParentID", "dbo.Dictionaries");
            DropForeignKey("dbo.Invoices", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleID", "dbo.Dictionaries");
            DropForeignKey("dbo.Invoices", "PayerID", "dbo.Dictionaries");
            DropForeignKey("dbo.Invoices", "MessageTypeID", "dbo.Dictionaries");
            DropForeignKey("dbo.Invoices", "MessageModeID", "dbo.Dictionaries");
            DropForeignKey("dbo.Invoices", "FormOfPaymentID", "dbo.Dictionaries");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Users", "IX_ContractNumberUniq");
            DropIndex("dbo.Invoices", new[] { "UserID" });
            DropIndex("dbo.Invoices", new[] { "FormOfPaymentID" });
            DropIndex("dbo.Invoices", new[] { "PayerID" });
            DropIndex("dbo.Invoices", new[] { "MessageModeID" });
            DropIndex("dbo.Invoices", new[] { "MessageTypeID" });
            DropIndex("dbo.Invoices", "IX_InvoiceNumberUniq");
            Sql("DROP TRIGGER [OnDictionariesHierarchyCalc]");
            DropIndex("dbo.Dictionaries", new[] { "ParentID" });
            DropTable("dbo.Users");
            DropTable("dbo.Invoices");
            DropTable("dbo.Dictionaries");
        }
    }
}
