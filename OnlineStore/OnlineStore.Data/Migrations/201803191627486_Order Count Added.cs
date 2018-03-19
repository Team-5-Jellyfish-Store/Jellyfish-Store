namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderCountAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ProductsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ProductsCount");
        }
    }
}
