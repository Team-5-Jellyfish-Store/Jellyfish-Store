using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Commands
{
    public class SearchCategoryCommand : ICommand
    {
        private readonly IOnlineStoreContext context;
        private readonly IReader reader;
        private readonly IWriter writer;

        public SearchCategoryCommand(IOnlineStoreContext context, IReader reader, IWriter writer)
        {
            this.context = context;
            this.reader = reader;
            this.writer = writer;
        }
        public string ExecuteThisCommand(string[] commandParameters)
        {
            this.writer.WriteLine("Please enter category name to view all products in this category");
            var categories = this.context.Categories.ToList();
            var categoryName = this.reader.Read();

            if (categories.Where(x => x.Name == categoryName).Count() ==0)
            {
                return "No such category!\r\n";
            }
           
            var products = this.context.Products.ToList();
            var matchingProducts = products.Where(x => x.Category.Name == categoryName);
            writer.WriteLine("Name / SellingPrice");
            foreach (var item in matchingProducts)
            {
               

                writer.Write(item.Name + "  ");
                writer.WriteLine(item.SellingPrice.ToString());
            }
            return "";
        }
    }
}
