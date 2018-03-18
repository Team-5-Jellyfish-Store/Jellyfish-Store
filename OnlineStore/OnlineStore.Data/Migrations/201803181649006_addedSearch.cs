namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSearch : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Couriers", "Phone", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Suppliers", "Phone", c => c.String(nullable: false, maxLength: 13));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suppliers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Couriers", "Phone", c => c.String(nullable: false));
        }
    }
}
