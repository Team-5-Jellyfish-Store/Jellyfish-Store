using System;

namespace OnlineStore.DTO.OrderModels.Constracts
{
    public interface IOrderModel
    {
        string Comment { get; set; }

        DateTime OrderedOn { get; set; }

        Nullable<DateTime> DeliveredOn { get; set; }

        string Username { get; set; }
    }
}
