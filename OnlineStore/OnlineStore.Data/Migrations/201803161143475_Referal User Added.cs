namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReferalUserAdded : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "ClientId", newName: "UserId");
            RenameIndex(table: "dbo.Orders", name: "IX_ClientId", newName: "IX_UserId");
            AddColumn("dbo.Users", "ReferalUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "ReferalUserId");
            AddForeignKey("dbo.Users", "ReferalUserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ReferalUserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "ReferalUserId" });
            DropColumn("dbo.Users", "ReferalUserId");
            RenameIndex(table: "dbo.Orders", name: "IX_UserId", newName: "IX_ClientId");
            RenameColumn(table: "dbo.Orders", name: "UserId", newName: "ClientId");
        }
    }
}
