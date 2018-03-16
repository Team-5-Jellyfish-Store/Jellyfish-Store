namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplierNameColumnsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Suppliers", "Firstname", c => c.String());
            AddColumn("dbo.Suppliers", "Lastname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "Lastname");
            DropColumn("dbo.Suppliers", "Firstname");
        }
    }
}
