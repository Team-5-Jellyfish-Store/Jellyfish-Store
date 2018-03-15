namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedtableOrderProduct : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductOrders", newName: "OrderProducts");
            DropPrimaryKey("dbo.OrderProducts");
            AddPrimaryKey("dbo.OrderProducts", new[] { "Order_Id", "Product_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OrderProducts");
            AddPrimaryKey("dbo.OrderProducts", new[] { "Product_Id", "Order_Id" });
            RenameTable(name: "dbo.OrderProducts", newName: "ProductOrders");
        }
    }
}
