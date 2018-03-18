namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPhoneProperty : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Username" });
            AddColumn("dbo.Couriers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Couriers", "FirstName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Couriers", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Suppliers", "Firstname", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Suppliers", "Lastname", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Suppliers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Suppliers", "Phone", c => c.String());
            AlterColumn("dbo.Suppliers", "Lastname", c => c.String());
            AlterColumn("dbo.Suppliers", "Firstname", c => c.String());
            AlterColumn("dbo.Couriers", "LastName", c => c.String());
            AlterColumn("dbo.Couriers", "FirstName", c => c.String());
            DropColumn("dbo.Couriers", "Phone");
            CreateIndex("dbo.Users", "Username", unique: true);
        }
    }
}
