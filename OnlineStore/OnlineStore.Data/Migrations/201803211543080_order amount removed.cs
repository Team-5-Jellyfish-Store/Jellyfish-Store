namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderamountremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
