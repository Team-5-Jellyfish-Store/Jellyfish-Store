namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userphonecolumndropped : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Phone" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Users", "Phone", unique: true);
        }
    }
}
