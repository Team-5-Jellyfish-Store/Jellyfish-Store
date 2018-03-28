using System;

namespace OnlineStore.DTO.OrderModels.Constracts
{
    public interface IOrderModel
    {
        int ProductsCount { get; set; }

        string Comment { get; set; }

        DateTime OrderedOn { get; set; }

        Nullable<DateTime> DeliveredOn { get; set; }

        string Username { get; set; }
    }
}
