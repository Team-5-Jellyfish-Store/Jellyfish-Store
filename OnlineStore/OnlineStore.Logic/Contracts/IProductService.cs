﻿using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.ProductModels.Contracts;
using System.Collections.Generic;

namespace OnlineStore.Logic.Contracts
{
    public interface IProductService
    {
        IEnumerable<IProductModel> GetAllProducts();
        IEnumerable<IProductModel> GetProductsByCategoryName(string categoryName);

        IProductModel FindProductByName(string name);

        void RemoveProductByName(string name);

        void AddProduct(IProductImportModel product);

        void AddProductRange(IEnumerable<IProductImportModel> products);

        bool ProductExistsByName(string productName);
    }
}