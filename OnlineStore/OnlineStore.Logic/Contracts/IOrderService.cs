using OnlineStore.DTO;
using OnlineStore.DTO.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Logic.Contracts
{
    public interface IOrderService
    {
        void MakeOrder(OrderMakeModel orderModel);

        IEnumerable<OrderModel> GetAllOrders();
    }
}
