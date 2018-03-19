namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedsuppliername : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Phone" });
            AddColumn("dbo.Suppliers", "Name", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Suppliers", "Firstname");
            DropColumn("dbo.Suppliers", "Lastname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Suppliers", "Lastname", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Suppliers", "Firstname", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Suppliers", "Name");
            CreateIndex("dbo.Users", "Phone", unique: true);
        }
    }
}
