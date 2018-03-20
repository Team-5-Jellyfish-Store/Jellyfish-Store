namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedIndexFromAddress : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Addresses", new[] { "AddressText" });
            CreateIndex("dbo.Products", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "Name" });
            CreateIndex("dbo.Addresses", "AddressText", unique: true);
        }
    }
}
