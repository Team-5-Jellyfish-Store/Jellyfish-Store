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
    public class SearchCategoryCommand : ICommand
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IReader reader;
        private readonly IWriter writer;

        public SearchCategoryCommand(ICategoryService categoryService, IProductService productService, IReader reader, IWriter writer)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.reader = reader;
            this.writer = writer;
        }
        public string ExecuteThisCommand()
        {
            this.writer.WriteLine("Please enter category name to view all products in this category");
            //var categories = this.productService.GetAllProducts(); add category model
            var categories = this.categoryService.GetAllCategories();
            var categoryName = this.reader.Read();

            if (!categories.Any(x => x.Name== categoryName))
            {
                return "No such category!\r\n";
            }

            var products = this.productService.GetAllProducts();
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
