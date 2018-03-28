using OnlineStore.DTO.OrderModels;
using OnlineStore.DTO.OrderModels.Constracts;
using System.Collections.Generic;

namespace OnlineStore.Logic.Contracts
{
    public interface IOrderService
    {
        void MakeOrder(IOrderMakeModel orderModel);

        IEnumerable<IOrderModel> GetAllOrders();
    }
}
