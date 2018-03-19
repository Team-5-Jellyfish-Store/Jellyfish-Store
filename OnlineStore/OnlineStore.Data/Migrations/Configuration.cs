using OnlineStore.Models;
using OnlineStore.Models.DataModels;
using OnlineStore.Models.Enums;

namespace OnlineStore.Data.Migrations
{
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

        protected override void Seed(OnlineStoreContext context)
        {
            var adminUser = new User
            {
                Username = "admin",
                Password = "52E1860E990048A44E5A8664395E709F0B786577199EB1DC0180D285A113BAD6",
                FirstName = "Pesho",
                LastName = "Bradata",
                EMail = "a@a.a",
                Role = UserRole.Admin,
                Address = new Address() {AddressText = "Server room 1", Town = new Town() {Name = "Plovdiv"}}
            };

            var firstSupplier = new Supplier
            {
                Name = "Awful Creatures Ltd.",
                Phone = "+35929110101",
                Address = new Address() { AddressText = "Bronx 1", Town = new Town() { Name = "Varna" } }
            };
            context.Users.Add(adminUser);
            context.Suppliers.Add(firstSupplier);
            context.SaveChanges();
        }
    }
}
