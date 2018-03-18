using System.Data.Entity;
using OnlineStore.Data.Contracts;
using OnlineStore.Models;

namespace OnlineStore.Data
{
    public class OnlineStoreContext : DbContext, IOnlineStoreContext
    {

        public OnlineStoreContext()
            : base("OnlineStore")
        {
            //var strategy = new DropCreateDatabaseAlways<OnlineStoreContext>();
            //Database.SetInitializer(strategy);
        }
        
        public IDbSet<User> Users { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Courier> Couriers { get; set; }
        public IDbSet<Supplier> Suppliers { get; set; }
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<Town> Towns { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasMany(or => or.Products).WithMany(pr => pr.Orders);

            modelBuilder.Entity<Address>().HasMany(t => t.Users).WithRequired(cl => cl.Address).WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>().HasMany(t => t.Couriers).WithRequired(courier=> courier.Address).WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>().HasMany(t => t.Suppliers).WithRequired(sup => sup.Address).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Product>()
            //    .HasRequired(s => s.Supplier)
            //    .WithMany(g => g.Products)
            //    .HasForeignKey(s => s.SupplierId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
