﻿using System.Data.Entity;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Data.Contracts
{
    public interface IOnlineStoreContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Category> Categories { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<OrderProduct> OrderProducts { get; set; }
        IDbSet<Courier> Couriers { get; set; }
        IDbSet<Supplier> Suppliers { get; set; }
        IDbSet<Town> Towns { get; set; }
        IDbSet<Address> Addresses { get; set; }

        int SaveChanges();
    }
}