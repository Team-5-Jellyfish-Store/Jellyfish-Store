using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineStore.Data.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Logic
{
    public class OrderService:IOrderService
    {
        private readonly IOnlineStoreContext context;

        public OrderService(IOnlineStoreContext context)
        {
            this.context = context;
        }


        public IEnumerable<OrderModel> GetAllOrders()
        {
            return context.Orders.ProjectTo<OrderModel>();
        }
    }
}

