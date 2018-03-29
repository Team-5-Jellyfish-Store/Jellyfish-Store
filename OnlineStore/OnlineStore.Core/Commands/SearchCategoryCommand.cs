using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using System;
using System.Linq;
using System.Text;

namespace OnlineStore.Core.Commands
{
    public class SearchCategoryCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly IReader reader;
        private readonly IWriter writer;

        public SearchCategoryCommand(IProductService productService, IReader reader, IWriter writer)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }
        public string ExecuteThisCommand()
        {
            this.writer.WriteLine("Please enter category name to view all products in this category");
            var categoryName = this.reader.Read();
            
            var matchingProducts = this.productService.GetProductsByCategoryName(categoryName);

            var results = new StringBuilder();

            results.AppendLine($"Items in category {categoryName}");

            if (matchingProducts.Any())
            {
                foreach (var item in matchingProducts)
                {
                    results.AppendLine(
                        $"Name: {item.Name}" + Environment.NewLine
                        + $"Selling price: ${item.SellingPrice}" + Environment.NewLine);
                }
            }
            else
            {
                results.AppendLine("No products!");
            }
            
            return results.ToString().Trim();
        }
    }
}
