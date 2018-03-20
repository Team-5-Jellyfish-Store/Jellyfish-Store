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
            var categoryName = this.reader.Read();
            var category = categoryService.FindCategoryByName(categoryName);

            var products = this.productService.GetAllProducts();
            var matchingProducts = products.Where(x => x.CategoryName == categoryName);

            writer.WriteLine("Name / SellingPrice / Category");
            foreach (var item in matchingProducts)
            {
                writer.Write(item.Name + "  ");
                writer.Write(item.SellingPrice.ToString());
                writer.WriteLine(item.CategoryName);
            }
            return "";
        }
    }
}
