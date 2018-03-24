using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class AddProductToProductsCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly IUserSession userSession;

        private readonly IReader reader;
        private readonly IWriter writer;

        public AddProductToProductsCommand(IProductService productService, IUserSession userSession, IReader reader, IWriter writer)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(IProductService));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(IUserSession));
            this.reader = reader ?? throw new ArgumentNullException(nameof(IReader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(IWriter));
        }

        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasSomeoneLogged())
            {
                return "Login first!";
            }

            if (!this.userSession.HasAdminRights())
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