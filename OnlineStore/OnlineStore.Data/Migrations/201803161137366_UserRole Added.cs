namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoleAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Role");
        }
    }
}
