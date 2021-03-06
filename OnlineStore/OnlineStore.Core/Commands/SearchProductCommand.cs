﻿using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using System;

namespace OnlineStore.Core.Commands
{
    public class SearchProductCommand : ICommand
    {

        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IProductService productService;

        public SearchProductCommand(IReader reader, IWriter writer, IProductService productService)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public string ExecuteThisCommand()
        {
            this.writer.WriteLine("Please enter product name to search for it");
            var searchedProduct = reader.Read();

            var matchingProduct = productService.FindProductByName(searchedProduct);
            var result = string.Empty;
            if (matchingProduct != null)
            {
                result = $"Name: {matchingProduct.Name}" + Environment.NewLine
              + $"Selling price: ${Math.Round(matchingProduct.SellingPrice, 2)} per item" + Environment.NewLine
              + $"Category: {matchingProduct.CategoryName}";
            }
            return result;
        }
    }
}
