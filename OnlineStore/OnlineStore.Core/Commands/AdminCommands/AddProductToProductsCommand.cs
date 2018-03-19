using System;
using System.Linq;
using OnlineStore.Core.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Models.DataModels;

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
            this.sessionService = sessionService ?? throw new ArgumentNullException(nameof(IUserSessionService));
            this.context = context ?? throw new ArgumentNullException(nameof(IOnlineStoreContext));
            this.reader = reader ?? throw new ArgumentNullException(nameof(IReader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(IWriter));
        }

        public string ExecuteThisCommand()
        {
            if (this.sessionService.UserIsAdmin() || this.sessionService.UserIsModerator())
            {
                this.writer.Write("Please enter product name: ");
                var productName = this.reader.Read();
                this.writer.Write("Please enter product price: ");
                var purchasePrice = decimal.Parse(this.reader.Read());
                this.writer.Write("Please enter quantity: ");
                var quantity = int.Parse(this.reader.Read());
                this.writer.Write("Please enter category name: ");
                var categoryName = this.reader.Read();
                var categoryFound = this.context.Categories.FirstOrDefault(f => f.Name == categoryName) ?? CreateCategory(categoryName);

                this.writer.Write("Please enter supplier name: ");
                var supplierName = this.reader.Read();
                var supplierFound = this.context.Suppliers.FirstOrDefault(f => f.Name == supplierName) ?? CreateSupplier(supplierName);

                var product = new Product
                {
                    Name = productName,
                    PurchasePrice = purchasePrice,
                    SellingPrice = purchasePrice * 1.5m,
                    Quantity = quantity,
                    Category = categoryFound,
                    Supplier = supplierFound
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

        private Category CreateCategory(string categoryName)
        {
            var categoryToAdd = new Category { Name = categoryName };
            this.context.Categories.Add(categoryToAdd);
            this.context.SaveChanges();
            var addedCategory = this.context.Categories.First(f => f.Name == categoryName);
            return addedCategory;
        }

        private Supplier CreateSupplier(string supplierName)
        {
            this.writer.WriteLine("Supplier not found. You should provide us some data in order to add it:");
            this.writer.Write("Please enter supplier town: ");
            var town = this.reader.Read();
            var townFound = this.context.Towns.FirstOrDefault(f => f.Name == town);
            this.writer.Write("Please enter supplier address: ");
            var addressText = this.reader.Read();

            this.writer.Write("Please enter supplier phone: ");
            var supplierPhone = this.reader.Read();

            var supplierToAdd = new Supplier
            {
                Name = supplierName,
                Phone = supplierPhone,
                Address = new Address
                {
                    AddressText = addressText,
                    Town = townFound ?? new Town { Name = town }
                }
            };
            this.context.Suppliers.Add(supplierToAdd);
            this.context.SaveChanges();

            var addedSupplier = this.context.Suppliers.First(f => f.Name == supplierName);
            return addedSupplier;
        }
    }
}
