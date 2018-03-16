namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsuppliertotable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers", "TownId", "dbo.Towns");
            DropIndex("dbo.Orders", new[] { "SupplierId" });
            DropIndex("dbo.Suppliers", new[] { "TownId" });
            CreateTable(
                "dbo.Couriers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        TownId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Towns", t => t.TownId)
                .Index(t => t.TownId);
            
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "SupplierId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "CourierId", c => c.Int(nullable: false));
            AddColumn("dbo.Suppliers", "Phone", c => c.String());
            CreateIndex("dbo.Products", "SupplierId");
            CreateIndex("dbo.Orders", "CourierId");
            AddForeignKey("dbo.Orders", "CourierId", "dbo.Couriers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "SupplierId");
            DropColumn("dbo.Suppliers", "FirstName");
            DropColumn("dbo.Suppliers", "LastName");
            DropColumn("dbo.Suppliers", "TownId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Suppliers", "TownId", c => c.Int(nullable: false));
            AddColumn("dbo.Suppliers", "LastName", c => c.String());
            AddColumn("dbo.Suppliers", "FirstName", c => c.String());
            AddColumn("dbo.Orders", "SupplierId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Couriers", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Orders", "CourierId", "dbo.Couriers");
            DropIndex("dbo.Couriers", new[] { "TownId" });
            DropIndex("dbo.Orders", new[] { "CourierId" });
            DropIndex("dbo.Products", new[] { "SupplierId" });
            DropColumn("dbo.Suppliers", "Phone");
            DropColumn("dbo.Orders", "CourierId");
            DropColumn("dbo.Products", "SupplierId");
            DropColumn("dbo.Products", "Price");
            DropTable("dbo.Couriers");
            CreateIndex("dbo.Suppliers", "TownId");
            CreateIndex("dbo.Orders", "SupplierId");
            AddForeignKey("dbo.Suppliers", "TownId", "dbo.Towns", "Id");
            AddForeignKey("dbo.Orders", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
        }
    }
}
