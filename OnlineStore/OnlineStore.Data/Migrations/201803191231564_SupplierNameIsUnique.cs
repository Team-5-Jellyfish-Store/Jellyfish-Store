namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplierNameIsUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Suppliers", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Suppliers", new[] { "Name" });
        }
    }
}
