using OnlineStore.Core.Contracts;
using OnlineStore.Core.Providers;
using OnlineStore.DTO.OrderModels;
using OnlineStore.Logic.Contracts;
using System;
using System.Text;

namespace OnlineStore.Core.Commands
{
    public class AddOrderCommand : ICommand
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly IUserSession userSession;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly DatetimeProvider datetime;

        public AddOrderCommand(IOrderService orderService, IProductService productService, IUserSession userSession, IWriter writer, IReader reader, DatetimeProvider datetime)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.datetime = datetime ?? throw new ArgumentNullException(nameof(datetime));
        }

        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasSomeoneLogged())
            {
                throw new ArgumentException("Login first!");
            }

            var orderModel = new OrderMakeModel();
            var orderResult = new StringBuilder();

            string productName = string.Empty;
            int productCount = new int();
            string moreProducts = string.Empty;
            do
            {
                this.writer.Write("Product: ");
                productName = this.reader.Read();
                var product = this.productService.FindProductByName(productName);

                if (!orderModel.ProductNameAndCounts.ContainsKey(product.Name))
                {
                    orderModel.ProductNameAndCounts.Add(product.Name, 0);
                }

                this.writer.Write("Count: ");
                productCount = int.Parse(this.reader.Read());
                if (productCount < 1)
                {
                    throw new ArgumentException("Product count cannot be negative!");
                }

                orderModel.ProductNameAndCounts[product.Name] += productCount;

                orderResult.AppendLine($"{product.Name}: {productCount}");

                this.writer.Write("More products? (y/n): ");
                moreProducts = this.reader.Read();
            } while (moreProducts.ToLower() == "y");

            this.writer.Write("Comment: ");
            string comment = this.reader.Read();
            orderModel.Comment = comment;

            var username = this.userSession.GetLoggedUserName();
            orderModel.Username = username;

            var orderedOn = this.datetime.Now;
            orderModel.OrderedOn = orderedOn;

            this.orderService.MakeOrder(orderModel);

            return $"User {username} ordered:\n{orderResult.ToString()}On: {orderModel.OrderedOn}";
        }
    }
}
