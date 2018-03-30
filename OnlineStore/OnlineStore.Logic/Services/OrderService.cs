using AutoMapper.QueryableExtensions;
using OnlineStore.Logic.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using OnlineStore.DTO.OrderModels;
using OnlineStore.DTO.OrderModels.Constracts;

namespace OnlineStore.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOnlineStoreContext context;

        public OrderService(IOnlineStoreContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void MakeOrder(IOrderMakeModel orderModel)
        {
            if (orderModel == null)
            {
                throw new ArgumentNullException(nameof(orderModel));
            }

            var user = this.context.Users.SingleOrDefault(x => x.Username == orderModel.Username)
                ?? throw new ArgumentException("User not found!");
                

            var courier = this.context.Couriers.FirstOrDefault()
                ?? throw new ArgumentException("No couriers found!");

            var orderToAdd = new Order()
            {
                Comment = orderModel.Comment,
                OrderedOn = orderModel.OrderedOn,
                User = user,
                Courier = courier,
            };

            this.context.Orders.Add(orderToAdd);

            var orderProducts = new List<OrderProduct>();

            foreach (var productNameAndCount in orderModel.ProductNameAndCounts)
            {
                var productName = productNameAndCount.Key;
                var productCount = productNameAndCount.Value;

                var product = this.context.Products.SingleOrDefault(x => x.Name == productName)
                    ?? throw new ArgumentException($"Product with name {productName} don't exists!");

                if (product.Quantity < productCount)
                {
                    throw new ArgumentException($"Product {productName} quantity not enough for that order!");
                }

                var orderProduct = new OrderProduct()
                {
                    Order = orderToAdd,
                    Product = product,
                    ProductCount = productCount
                };

                orderProducts.Add(orderProduct);

                product.Quantity -= productCount;
            }

            orderProducts.ForEach(o => this.context.OrderProducts.Add(o));

            this.context.SaveChanges();
        }

        public IEnumerable<IOrderModel> GetAllOrders()
        {
            return context.Orders.ProjectTo<OrderModel>();
        }
    }
}

