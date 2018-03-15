namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TownsTableAddded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clients", "TownId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Comment", c => c.String());
            AddColumn("dbo.Orders", "OrderedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Suppliers", "TownId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "TownId");
            CreateIndex("dbo.Suppliers", "TownId");
            AddForeignKey("dbo.Clients", "TownId", "dbo.Towns", "Id");
            AddForeignKey("dbo.Suppliers", "TownId", "dbo.Towns", "Id");
            DropColumn("dbo.Clients", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Address", c => c.String());
            DropForeignKey("dbo.Suppliers", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Clients", "TownId", "dbo.Towns");
            DropIndex("dbo.Suppliers", new[] { "TownId" });
            DropIndex("dbo.Clients", new[] { "TownId" });
            DropColumn("dbo.Suppliers", "TownId");
            DropColumn("dbo.Orders", "OrderedOn");
            DropColumn("dbo.Orders", "Comment");
            DropColumn("dbo.Clients", "TownId");
            DropTable("dbo.Towns");
        }
    }
}
