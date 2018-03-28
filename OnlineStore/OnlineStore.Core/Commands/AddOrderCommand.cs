using OnlineStore.Core.Contracts;
using OnlineStore.Core.Providers.Providers;
using OnlineStore.DTO.Factory;
using OnlineStore.DTO.OrderModels;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Core.Commands
{
    public class AddOrderCommand : ICommand
    {
        private readonly string UserOrderSuccessMessage = "User {0} ordered:\n{1}On: {2}";

        private readonly string NoLoggedUserFailMessage = "Login first!";
        private readonly string NegativeProductCountFailMessage = "Product count cannot be negative!";
        private readonly string InvalidValuesInModelFailMessage = "Invalid model! Please provide valid values!";

        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly IUserSession userSession;
        private readonly IDataTransferObjectFactory dataTransferObjectFactory;
        private readonly IValidator validator;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly DatetimeProvider datetime;

        public AddOrderCommand(IOrderService orderService, IProductService productService, IUserSession userSession, IDataTransferObjectFactory dataTransferObjectFactory, IValidator validator, IWriter writer, IReader reader, DatetimeProvider datetime)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            this.dataTransferObjectFactory = dataTransferObjectFactory ?? throw new ArgumentNullException(nameof(dataTransferObjectFactory));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.datetime = datetime ?? throw new ArgumentNullException(nameof(datetime));
        }

        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasSomeoneLogged())
            {
                throw new ArgumentException(this.NoLoggedUserFailMessage);
            }
            
            var orderResult = new StringBuilder();

            var productNameAndCounts = new Dictionary<string, int>();

            string productName = string.Empty;
            int productCount = new int();
            string moreProducts = string.Empty;
            do
            {
                this.writer.Write("Product: ");
                productName = this.reader.Read();
                var product = this.productService.FindProductByName(productName);

                if (!productNameAndCounts.ContainsKey(product.Name))
                {
                    productNameAndCounts.Add(product.Name, 0);
                }

                this.writer.Write("Count: ");
                productCount = int.Parse(this.reader.Read());

                if (productCount < 1)
                {
                    throw new ArgumentException(this.NegativeProductCountFailMessage);
                }

                productNameAndCounts[product.Name] += productCount;

                orderResult.AppendLine($"{product.Name}: {productCount}");

                this.writer.Write("More products? (y/n): ");
                moreProducts = this.reader.Read();
            } while (moreProducts.ToLower() == "y");

            this.writer.Write("Comment: ");
            string comment = this.reader.Read();

            var username = this.userSession.GetLoggedUserName();

            var orderedOn = this.datetime.Now;

            var orderModel = this.dataTransferObjectFactory.CreateOrderMakeModel(productNameAndCounts, comment, username, orderedOn);

            if (!this.validator.IsValid(orderModel))
            {
                throw new ArgumentException(this.InvalidValuesInModelFailMessage);
            }

            this.orderService.MakeOrder(orderModel);

            return string.Format(this.UserOrderSuccessMessage, username, orderResult.ToString(), orderModel.OrderedOn);
        }
    }
}
