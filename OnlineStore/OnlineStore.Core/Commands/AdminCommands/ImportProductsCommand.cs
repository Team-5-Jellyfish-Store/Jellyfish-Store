using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;
using OnlineStore.DTO.ExternalImportDto;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class ImportProductsCommand : ICommand
    {
        private readonly IUserSession userSession;
        private readonly IOnlineStoreContext context;
        private readonly IValidator validator;

        public ImportProductsCommand(IUserSession userSession, IOnlineStoreContext context, IValidator validator)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(IValidator));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(IUserSession));
            this.context = context ?? throw new ArgumentNullException(nameof(IOnlineStoreContext));
        }

        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasSomeoneLogged())
            {
                return "Login first!";
            }

            if (this.userSession.HasAdminRights())
            {
                const string failureMessage = "Import rejected. Input is with invalid format.";
                const string doubleEntryMessage = "Import rejected. Duplicate courier found!";

                var importString = File.ReadAllText("../../../Datasets/Products.json");
                var deserializedProducts = JsonConvert.DeserializeObject<ProductImportDto[]>(importString);
                var importResults = new StringBuilder();

                var validProducts = new List<Product>();

                foreach (var productDto in deserializedProducts)
                {
                    if (!this.validator.IsValid(productDto))
                    {
                        importResults.AppendLine(failureMessage);
                        continue;
                    }

                    if (this.context.Products.Any(a => a.Name == productDto.Name))
                    {
                        importResults.AppendLine(doubleEntryMessage);
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
                        var categoryToAdd = new Category { Name = productDto.Category };
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
                var result = importResults.ToString().Trim();
                return result;
            }
            else
            {
                return "User must be admin or moderator in order to import data!";
            }
        }
    }
}
