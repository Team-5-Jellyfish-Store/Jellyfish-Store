using System;
using System.Linq;
using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Models;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class AddProductToProductsCommand : ICommand
    {
        private readonly IUserSessionService sessionService;
        private readonly IOnlineStoreContext context;
        private readonly IReader reader;
        private readonly IWriter writer;

        public AddProductToProductsCommand(IUserSessionService sessionService, IOnlineStoreContext context, IReader reader, IWriter writer)
        {
            this.sessionService = sessionService;
            this.context = context;
            this.reader = reader;
            this.writer = writer;
        }

        public string ExecuteThisCommand(string[] commandParameters)
        {
            if (this.sessionService.UserIsAdmin() || this.sessionService.UserIsModerator())
            {
                this.writer.Write("Please enter product name");
                var productName = this.reader.Read();
                this.writer.Write("Please enter product price");
                var purchasePrice = decimal.Parse(this.reader.Read());
                this.writer.Write("Please enter quantity");
                var quantity = int.Parse(this.reader.Read());
                this.writer.Write("Please enter category name");
                var category = this.reader.Read();
                var categoryFound = this.context.Categories.FirstOrDefault(f => f.Name == category);
                int categoryId;
                if (categoryFound == null)
                {
                    var categoryToAdd = new Category {Name = category};
                    this.context.Categories.Add(categoryToAdd);
                    this.context.SaveChanges();
                    categoryId = this.context.Categories.First(f => f.Name == category).Id;
                }
                else
                {
                    categoryId = categoryFound.Id;
                }
                var product = new Product()
                {
                    Name = productName,
                    PurchasePrice = purchasePrice,
                    Quantity = quantity,
                    CategoryId = categoryId
                };
                this.context.Products.Add(product);
                this.context.SaveChanges();
                return $"Product {productName} added successfully!";
            }
            else
            {
                return "User is neither admin nor moderator and cannot add products!";
            }
        }
    }
}
