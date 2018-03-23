using OnlineStore.Models;
using OnlineStore.Models.DataModels;
using OnlineStore.Models.Enums;

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
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(OnlineStoreContext context)
        {
            if (!context.Users.Any(x => x.Role == UserRole.Admin))
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Password = "52E1860E990048A44E5A8664395E709F0B786577199EB1DC0180D285A113BAD6",
                    FirstName = "Pesho",
                    LastName = "Bradata",
                    EMail = "a@a.a",
                    Role = UserRole.Admin,
                    Address = new Address() { AddressText = "Server room 1", Town = new Town() { Name = "Plovdiv" } }
                };

                var firstSupplier = new Supplier
                {
                    Name = "Awful Creatures Ltd.",
                    Phone = "+35929110101",
                    Address = new Address() { AddressText = "Staria Lebed 1", Town = new Town() { Name = "Varna" } }
                };
                context.Users.Add(adminUser);
                context.Suppliers.Add(firstSupplier);
                context.SaveChanges();
            }
        }
    }
}
