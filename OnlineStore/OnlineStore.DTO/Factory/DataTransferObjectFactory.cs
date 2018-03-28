using OnlineStore.DTO.OrderModels;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;
using System;
using System.Collections.Generic;

namespace OnlineStore.DTO.Factory
{
    public class DataTransferObjectFactory : IDataTransferObjectFactory
    {
        public OrderMakeModel CreateOrderMakeModel(IDictionary<string, int> productNameAndCounts, string comment, string username, DateTime orderedOn)
        {
            if (comment == string.Empty)
            {
                comment = null;
            }

            return new OrderMakeModel()
            {
                Comment = comment,
                ProductNameAndCounts = productNameAndCounts,
                OrderedOn = orderedOn,
                Username = username
            };
        }

        public IProductImportModel CreateProductImportModel(string productName, decimal purchasePrice, int quantity, string categoryName, string supplierName)
        {
            return new ProductImportModel()
            {
                Name = productName,
                PurchasePrice = purchasePrice,
                Quantity = quantity,
                CategoryName = categoryName,
                SupplierName = supplierName
            };
        }

        public IUserRegisterModel CreateUserRegisterModel(string username, string email, string password, string firstName, string lastName, string townName, string addressText)
        {
            if (firstName == string.Empty)
            {
                firstName = null;
            }

            if (lastName == string.Empty)
            {
                lastName = null;
            }

            return new UserRegisterModel()
            {
                Username = username,
                EMail = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                AddressText = addressText,
                TownName = townName
            };
        }
    }
}
