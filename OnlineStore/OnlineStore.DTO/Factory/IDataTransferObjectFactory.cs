using OnlineStore.DTO.OrderModels;
using OnlineStore.DTO.OrderModels.Constracts;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;
using System;
using System.Collections.Generic;

namespace OnlineStore.DTO.Factory
{
    public interface IDataTransferObjectFactory
    {
        IOrderMakeModel CreateOrderMakeModel(IDictionary<string, int> productNameAndCounts, string comment, string username, DateTime orderedOn);

        IUserRegisterModel CreateUserRegisterModel(string username, string email, string password, string firstName, string lastName, string townName, string addressText);

        IProductImportModel CreateProductImportModel(string productName, decimal purchasePrice, int quantity, string categoryName, string supplierName);
    }
}
