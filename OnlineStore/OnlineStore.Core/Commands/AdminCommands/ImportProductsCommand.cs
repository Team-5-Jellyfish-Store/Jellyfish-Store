using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.DTO;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class ImportProductsCommand : ICommand
    {

        private readonly IUserSessionService sessionService;
        private readonly IOnlineStoreContext context;
        private readonly IValidator validator;


        public ImportProductsCommand(IUserSessionService sessionService, IOnlineStoreContext context, IValidator validator)
        {
            this.sessionService = sessionService;
            this.context = context;
            this.validator = validator;
        }

    public string ExecuteThisCommand()
        {
            //if (this.sessionService.UserIsAdmin() || this.sessionService.UserIsModerator())
            //{
                const string FailureMessage = "Data rejected. Input is with invalid format.";
                var importString = File.ReadAllText("../../../Datasets/Products.json");
                var deserializedProducts = JsonConvert.DeserializeObject<ProductImportDto[]>(importString);
                var importResults = new StringBuilder();

                var validProducts = new List<Product>();

                foreach (var productDto in deserializedProducts)
                {
                    if (!this.validator.IsValid(productDto))
                    {
                        importResults.AppendLine(FailureMessage);
                        continue;
                    }
                    var supplier = this.context.Suppliers.FirstOrDefault(a => a.Name == productDto.Supplier);
                    if (supplier == null)
                    {
                        importResults.AppendLine($"Supplier with name {productDto.Supplier} does not exist!");
                        continue;
                    }

                    if (!this.context.Categories.Any(a => a.Name == productDto.Category))
                    {
                        var categoryToAdd = new Category { Name = productDto.Category};
                        this.context.Categories.Add(categoryToAdd);
                        this.context.SaveChanges();
                    }
                    var productCategory = this.context.Categories.FirstOrDefault(f => f.Name == productDto.Category);

                    var productToAdd = new Product()
                    {
                        Name = productDto.Name,
                        PurchasePrice = productDto.PurchasePrice,
                        SellingPrice = productDto.PurchasePrice * 1.5m,
                        Quantity = productDto.Quantity,
                        Category = productCategory,
                        Supplier = supplier
                    };
                    validProducts.Add(productToAdd);
                    importResults.AppendLine($"{productDto.Quantity} items of product {productDto.Name} added successfully!");
                }

                validProducts.ForEach(c => this.context.Products.Add(c));
                this.context.SaveChanges();
                var result = importResults.ToString();
                return result;
            //}

            return "User must be admin or moderator in order to import data!";
        }
    }
}
