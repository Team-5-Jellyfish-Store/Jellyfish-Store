using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.DTO.ProductModels;
using OnlineStore.Core.Providers;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class AddProductToProductsCommand : ICommand
    {
        private readonly string ProductAddedSuccesMessage = "Product {0} added successfully!";
        private readonly string NoLoggedUserFailMessage = "Login first!";
        private readonly string UserHasNoRightsFailMessage = "User is neither admin nor moderator and cannot add products!";

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
                return this.NoLoggedUserFailMessage;
            }

            if (!this.userSession.HasAdminRights())
            {
                return this.UserHasNoRightsFailMessage;
            }

            var productModel = new ProductImportModel();

            this.writer.Write("Please enter product name: ");
            var productName = this.reader.Read();
            productModel.Name = productName;

            this.writer.Write("Please enter product price: ");
            var purchasePrice = decimal.Parse(this.reader.Read());
            productModel.PurchasePrice = purchasePrice;

            this.writer.Write("Please enter quantity: ");
            var quantity = int.Parse(this.reader.Read());
            productModel.Quantity = quantity;

            this.writer.Write("Please enter category name: ");
            var categoryName = this.reader.Read();
            productModel.CategoryName = categoryName;

            this.writer.Write("Please enter supplier name: ");
            var supplierName = this.reader.Read();
            productModel.SupplierName = supplierName;

            this.productService.AddProduct(productModel);

            return string.Format(this.ProductAddedSuccesMessage, productName);
        }
    }
}