using System;
using System.Collections.Generic;

namespace OnlineStore.DTO.OrderModels.Constracts
{
    public interface IOrderMakeModel
    {
        IDictionary<string, int> ProductNameAndCounts { get; set; }

        string Comment { get; set; }

        DateTime OrderedOn { get; set; }

        string Username { get; set; }
    }
}
