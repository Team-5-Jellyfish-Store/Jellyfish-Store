using OnlineStore.DTO.OrderModels;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.UserModels;
using System;
using System.Collections.Generic;

namespace OnlineStore.DTO.Factory
{
    public interface IDataTransferObjectFactory
    {
        OrderMakeModel CreateOrderMakeModel(IDictionary<string, int> productNameAndCounts, string comment, string username, DateTime orderedOn);

        UserRegisterModel CreateUserRegisterModel(string username, string email, string password, string firstName, string lastName, string townName, string addressText);

        ProductImportModel CreateProductImportModel(string productName, decimal purchasePrice, int quantity, string categoryName, string supplierName);
    }
}
