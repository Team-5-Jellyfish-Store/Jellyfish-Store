namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientRenamedToUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clients", newName: "Users");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Users", newName: "Clients");
        }
    }
}
