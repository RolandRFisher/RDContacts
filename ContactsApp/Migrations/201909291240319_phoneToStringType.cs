namespace ContactsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phoneToStringType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactNumbers", "Number", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactNumbers", "Number", c => c.Int(nullable: false));
        }
    }
}
