namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class morevalidations : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "ReferalUserId" });
            AddColumn("dbo.Users", "Phone", c => c.String(maxLength: 15));
            AlterColumn("dbo.Addresses", "AddressText", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Orders", "Comment", c => c.String(maxLength: 300));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Users", "FirstName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Users", "LastName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Users", "EMail", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "ReferalUserId", c => c.Int());
            AlterColumn("dbo.Towns", "Name", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.Addresses", "AddressText", unique: true);
            CreateIndex("dbo.Users", "Username", unique: true);
            CreateIndex("dbo.Users", "EMail", unique: true);
            CreateIndex("dbo.Users", "Phone", unique: true);
            CreateIndex("dbo.Users", "ReferalUserId");
            CreateIndex("dbo.Towns", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Towns", new[] { "Name" });
            DropIndex("dbo.Users", new[] { "ReferalUserId" });
            DropIndex("dbo.Users", new[] { "Phone" });
            DropIndex("dbo.Users", new[] { "EMail" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Addresses", new[] { "AddressText" });
            AlterColumn("dbo.Towns", "Name", c => c.String());
            AlterColumn("dbo.Users", "ReferalUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "EMail", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Orders", "Comment", c => c.String());
            AlterColumn("dbo.Addresses", "AddressText", c => c.String());
            DropColumn("dbo.Users", "Phone");
            CreateIndex("dbo.Users", "ReferalUserId");
        }
    }
}
