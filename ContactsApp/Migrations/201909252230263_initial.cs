namespace ContactsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactNumberType = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => new { t.Number, t.ContactId }, unique: true);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(nullable: false, maxLength: 254),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.EmailAddress, unique: true)
                .Index(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.ContactNumbers", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Emails", new[] { "ContactId" });
            DropIndex("dbo.Emails", new[] { "EmailAddress" });
            DropIndex("dbo.ContactNumbers", new[] { "Number", "ContactId" });
            DropTable("dbo.Emails");
            DropTable("dbo.ContactNumbers");
            DropTable("dbo.Contacts");
        }
    }
}
