using System.Data.Entity;
using OnlineStore.Data.Contracts;
using OnlineStore.Models;

namespace OnlineStore.Data
{
    public class OnlineStoreContext : DbContext, IOnlineStoreContext
    {

        public OnlineStoreContext()
            : base("OnlineStore") { }
        
        public IDbSet<Client> Clients { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Courier> Courriers { get; set; }
        public IDbSet<Supplier> Suppliers { get; set; }
        public IDbSet<Town> Towns { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasMany(or => or.Products).WithMany(pr => pr.Orders);

            modelBuilder.Entity<Town>().HasMany(t => t.Clients).WithRequired(cl => cl.Town).WillCascadeOnDelete(false);

            modelBuilder.Entity<Town>().HasMany(t => t.Couriers).WithRequired(courier=> courier.Town).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Product>()
            //    .HasRequired(s => s.Supplier)
            //    .WithMany(g => g.Products)
            //    .HasForeignKey(s => s.SupplierId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
