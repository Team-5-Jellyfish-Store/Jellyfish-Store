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

            writer.WriteLine("Name / SellingPrice / Category");
            writer.Write(matchingProduct.Name + "  ");
            writer.Write(matchingProduct.SellingPrice.ToString());
            writer.WriteLine(matchingProduct.CategoryName);


            return "";
        }
    }
}
