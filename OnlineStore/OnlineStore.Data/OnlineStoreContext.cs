

using OnlineStore.Models;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace OnlineStore.Data
{
    public class OnlineStoreContext : DbContext
    {
        public OnlineStoreContext() : base("OnlineStore")
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
