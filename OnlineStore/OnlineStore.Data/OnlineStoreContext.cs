﻿using System.Data.Entity;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;
using OnlineStore.Data.Migrations;

namespace OnlineStore.Data
{
    public class OnlineStoreContext : DbContext, IOnlineStoreContext
    {

        public OnlineStoreContext()
            : base("OnlineStore")
        {
            var strategy = new MigrateDatabaseToLatestVersion<OnlineStoreContext, Configuration>();
            Database.SetInitializer(strategy);
        }

        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<Order> Orders { get; set; }
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<OrderProduct> OrderProducts { get; set; }
        public virtual IDbSet<Courier> Couriers { get; set; }
        public virtual IDbSet<Supplier> Suppliers { get; set; }
        public virtual IDbSet<Address> Addresses { get; set; }
        public virtual IDbSet<Town> Towns { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasMany(t => t.Users).WithRequired(cl => cl.Address).WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>().HasMany(t => t.Couriers).WithRequired(courier => courier.Address).WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>().HasMany(t => t.Suppliers).WithRequired(sup => sup.Address).WillCascadeOnDelete(false);


            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderProducts);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderProducts);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasRequired(op => op.Product);

            modelBuilder.Entity<OrderProduct>()
                .HasRequired(op => op.Order);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
