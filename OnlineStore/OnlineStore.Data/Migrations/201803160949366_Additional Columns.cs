namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalColumns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Couriers", "TownId", "dbo.Towns");
            DropIndex("dbo.Clients", new[] { "TownId" });
            DropIndex("dbo.Couriers", new[] { "TownId" });
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressText = c.String(),
                        TownId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Towns", t => t.TownId, cascadeDelete: true)
                .Index(t => t.TownId);
            
            AddColumn("dbo.Orders", "DeliveredOn", c => c.DateTime());
            AddColumn("dbo.Clients", "EMail", c => c.String());
            AddColumn("dbo.Clients", "AddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Couriers", "AddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Suppliers", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "AddressId");
            CreateIndex("dbo.Couriers", "AddressId");
            CreateIndex("dbo.Suppliers", "AddressId");
            AddForeignKey("dbo.Clients", "AddressId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Couriers", "AddressId", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Suppliers", "AddressId", "dbo.Addresses", "Id");
            DropColumn("dbo.Clients", "TownId");
            DropColumn("dbo.Couriers", "TownId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Couriers", "TownId", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "TownId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Addresses", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Suppliers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Couriers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Clients", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Suppliers", new[] { "AddressId" });
            DropIndex("dbo.Couriers", new[] { "AddressId" });
            DropIndex("dbo.Clients", new[] { "AddressId" });
            DropIndex("dbo.Addresses", new[] { "TownId" });
            DropColumn("dbo.Suppliers", "AddressId");
            DropColumn("dbo.Couriers", "AddressId");
            DropColumn("dbo.Clients", "AddressId");
            DropColumn("dbo.Clients", "EMail");
            DropColumn("dbo.Orders", "DeliveredOn");
            DropTable("dbo.Addresses");
            CreateIndex("dbo.Couriers", "TownId");
            CreateIndex("dbo.Clients", "TownId");
            AddForeignKey("dbo.Couriers", "TownId", "dbo.Towns", "Id");
            AddForeignKey("dbo.Clients", "TownId", "dbo.Towns", "Id");
        }
    }
}
