using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class AddProductToProductsCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly IUserSessionService sessionService;

        private readonly IReader reader;
        private readonly IWriter writer;

        public AddProductToProductsCommand(IProductService productService, IUserSessionService sessionService, IReader reader, IWriter writer)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(IProductService));
            this.sessionService = sessionService ?? throw new ArgumentNullException(nameof(IUserSessionService));
            this.reader = reader ?? throw new ArgumentNullException(nameof(IReader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(IWriter));
        }

        public string ExecuteThisCommand()
        {
            if (!this.sessionService.UserIsAdmin() && !this.sessionService.UserIsModerator())
            {
                return "User is neither admin nor moderator and cannot add products!";
            }

            this.writer.Write("Please enter product name: ");
            var productName = this.reader.Read();
            this.writer.Write("Please enter product price: ");
            var purchasePrice = decimal.Parse(this.reader.Read());
            this.writer.Write("Please enter quantity: ");
            var quantity = int.Parse(this.reader.Read());
            this.writer.Write("Please enter category name: ");
            var categoryName = this.reader.Read();
            this.writer.Write("Please enter supplier name: ");
            var supplierName = this.reader.Read();

            this.productService.CreateProduct(productName, purchasePrice, quantity, categoryName,
            supplierName);
            return $"Product {productName} added successfully!";
        }
    }
}