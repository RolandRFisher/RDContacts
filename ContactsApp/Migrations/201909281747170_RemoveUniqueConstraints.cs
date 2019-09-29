namespace ContactsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUniqueConstraints : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ContactNumbers", new[] { "Number", "ContactId" });
            DropIndex("dbo.Emails", new[] { "EmailAddress" });
            CreateIndex("dbo.ContactNumbers", "ContactId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContactNumbers", new[] { "ContactId" });
            CreateIndex("dbo.Emails", "EmailAddress", unique: true);
            CreateIndex("dbo.ContactNumbers", new[] { "Number", "ContactId" }, unique: true);
        }
    }
}
