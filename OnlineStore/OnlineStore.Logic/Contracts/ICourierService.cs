using System.Collections.Generic;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ICourierService
    {
        void AddCourierRange(List<Courier> courier);

    }
}
