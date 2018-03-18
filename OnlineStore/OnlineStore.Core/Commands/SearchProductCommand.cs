using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Commands
{
    public class SearchProductCommand : ICommand
    {
        private readonly IOnlineStoreContext context;
        private readonly IReader reader;
        private readonly IWriter writer;

        public SearchProductCommand(IOnlineStoreContext context, IReader reader, IWriter writer)
        {
            this.context = context;
            this.reader = reader;
            this.writer = writer;
        }
        public string ExecuteThisCommand(string[] commandParameters)
        {
            this.writer.WriteLine("Please enter product name to search for it");
            var searchedProduct = reader.Read();
            var products = this.context.Products.ToList();

            if (products.Where(x => x.Name == searchedProduct).Count() == 0)
            {
                return "No such product exists!\r\n";
            }


            var matchingProducts = products.Find(x => x.Name == searchedProduct);

            writer.WriteLine("Name / SellingPrice");

            writer.Write(matchingProducts.Name + "  ");
            writer.WriteLine(matchingProducts.SellingPrice.ToString());

            return "";
        }
    }
}
