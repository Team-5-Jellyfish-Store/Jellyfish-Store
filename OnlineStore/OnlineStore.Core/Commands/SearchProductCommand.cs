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

            var products = productService.GetAllProducts();

            if (products.Where(x => x.Name == searchedProduct).Count() == 0)
            {
                return "No such product exists!\r\n";
            }


            //var matchingProducts = products.find(x => x.Name == searchedProduct);
            var matchingProducts = products.FirstOrDefault(x => x.Name == searchedProduct);


            writer.WriteLine("Name / SellingPrice");

            writer.Write(matchingProducts.Name + "  ");
            writer.WriteLine(matchingProducts.SellingPrice.ToString());
           writer.WriteLine(matchingProducts.Category.Pesho.ToString());

            return "";
        }
    }
}
