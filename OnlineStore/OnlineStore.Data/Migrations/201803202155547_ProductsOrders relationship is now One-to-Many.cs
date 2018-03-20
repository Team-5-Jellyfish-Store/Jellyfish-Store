namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsOrdersrelationshipisnowOnetoMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderProducts", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderProducts", "Product_Id", "dbo.Products");
            DropIndex("dbo.OrderProducts", new[] { "Order_Id" });
            DropIndex("dbo.OrderProducts", new[] { "Product_Id" });
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ProductId");
            AddForeignKey("dbo.Orders", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropTable("dbo.OrderProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Product_Id });
            
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropColumn("dbo.Orders", "ProductId");
            CreateIndex("dbo.OrderProducts", "Product_Id");
            CreateIndex("dbo.OrderProducts", "Order_Id");
            AddForeignKey("dbo.OrderProducts", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderProducts", "Order_Id", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
