﻿using System.Collections.Generic;
using OnlineStore.DTO.CourierModels;

namespace OnlineStore.Logic.Contracts
{
    public interface ICourierService
    {
        void AddCourierRange(IList<ICourierImportModel> courier);

        bool CourierExistsByName(string firstName, string lastName);
    }
}
