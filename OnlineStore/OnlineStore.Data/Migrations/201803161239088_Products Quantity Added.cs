namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsQuantityAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 64));
            CreateIndex("dbo.Users", "Username", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Username" });
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Products", "Quantity");
        }
    }
}
