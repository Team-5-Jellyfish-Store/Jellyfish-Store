namespace OnlineStore.Data.Migrations
{
    using OnlineStore.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineStore.Data.OnlineStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OnlineStore.Data.OnlineStoreContext context)
        {
        }
    }
}
