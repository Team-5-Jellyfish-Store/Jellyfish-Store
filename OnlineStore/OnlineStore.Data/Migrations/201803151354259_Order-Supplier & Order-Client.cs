namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderSupplierOrderClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Address", c => c.String());
            AddColumn("dbo.Orders", "SupplierId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "SupplierId");
            AddForeignKey("dbo.Orders", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
            DropColumn("dbo.Suppliers", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Suppliers", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Orders", new[] { "SupplierId" });
            DropColumn("dbo.Orders", "SupplierId");
            DropColumn("dbo.Clients", "Address");
        }
    }
}
