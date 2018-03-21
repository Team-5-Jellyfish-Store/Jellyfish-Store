using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Logic.Contracts;

﻿using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Models.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace OnlineStore.Logic
{
    public class OrderService : IOrderService
    {
        private readonly IOnlineStoreContext context;

        public OrderService(IOnlineStoreContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void MakeOrder(OrderMakeModel orderModel)
        {
            //if (orderModel == null)
            //{
            //    throw new ArgumentNullException(nameof(orderModel));
            //}

            //var product = this.context.Products.SingleOrDefault(x => x.Name == orderModel.ProductName)
            //    ?? throw new ArgumentException("Product with that name don't exists!");

            //if (product.Quantity < orderModel.ProductCount)
            //{
            //    throw new ArgumentException("Product quantity not enough for that order!");
            //}

            //var user = this.context.Users.SingleOrDefault(x => x.Username == orderModel.Username)
            //    ?? throw new ArgumentException("User not found!");

            //var courier = this.context.Couriers.FirstOrDefault()
            //    ?? throw new ArgumentException("No couriers found!");

            //var amount = product.SellingPrice * orderModel.ProductCount;

            //var order = new Order()
            //{
            //    Product = product,
            //    ProductsCount = orderModel.ProductCount,
            //    Comment = orderModel.Comment,
            //    OrderedOn = orderModel.OrderedOn,
            //    Amount = amount,
            //    User = user,
            //    Courier = courier
            //};

            //this.context.Orders.Add(order);

            ////user.Orders.Add(order);

            //product.Quantity -= orderModel.ProductCount;

            //context.SaveChanges();
        }

        public IEnumerable<OrderModel> GetAllOrders()
        {
            return context.Orders.ProjectTo<OrderModel>();
        }
    }
}

