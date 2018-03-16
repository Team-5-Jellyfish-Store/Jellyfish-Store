﻿using System.Data.Entity;
using OnlineStore.Models;

namespace OnlineStore.Data.Contracts
{
    public interface IOnlineStoreContext
    {
        IDbSet<User> Clients { get; set; }
        IDbSet<Category> Categories { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<Courier> Courriers { get; set; }
        IDbSet<Supplier> Suppliers { get; set; }
        IDbSet<Town> Towns { get; set; }
        IDbSet<Address> Addresses { get; set; }

        int SaveChanges();
    }
}