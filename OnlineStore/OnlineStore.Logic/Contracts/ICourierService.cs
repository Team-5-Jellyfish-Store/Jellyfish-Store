using System.Collections.Generic;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.CourierModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ICourierService
    {
        void AddCourierRange(IList<CourierImportModel> courier);
    }
}
