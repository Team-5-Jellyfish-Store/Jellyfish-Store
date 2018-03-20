using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic;
using OnlineStore.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Commands
{
    public class SearchProductCommand : ICommand
    {

        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IProductService productService;

        public SearchProductCommand(IReader reader, IWriter writer, IProductService productService)
        {
            this.reader = reader;
            this.writer = writer;
            this.productService = productService;
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
