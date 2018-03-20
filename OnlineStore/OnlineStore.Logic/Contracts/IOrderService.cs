using OnlineStore.DTO;

namespace OnlineStore.Logic.Contracts
{
    public interface IOrderService
    {
        void MakeOrder(OrderModel orderModel);
    }
}
