using OnlineStore.DTO.OrderModels;
using System.Collections.Generic;

namespace OnlineStore.Logic.Contracts
{
    public interface IOrderService
    {
        void MakeOrder(OrderMakeModel orderModel);

        IEnumerable<OrderModel> GetAllOrders();
    }
}
