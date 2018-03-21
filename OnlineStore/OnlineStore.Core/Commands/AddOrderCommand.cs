using OnlineStore.Core.Contracts;
using OnlineStore.DTO;
using OnlineStore.Logic.Contracts;
using System;
using System.Linq;

namespace OnlineStore.Core.Commands
{
    public class AddOrderCommand : ICommand
    {
        private readonly IOrderService orderService;
        private readonly IValidator validator;
        private readonly IUserSessionService userSession;
        private readonly IWriter writer;
        private readonly IReader reader;

        public AddOrderCommand(IOrderService orderService, IValidator validator, IUserSessionService userSession, IWriter writer, IReader reader)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public string ExecuteThisCommand()
        {
            var loggedUser = userSession.GetLoggedUser() ?? throw new ArgumentException("No logged user!");

            this.writer.Write("Product: ");
            string productName = this.reader.Read();
            productName = validator.ValidateValue(productName, true);

            this.writer.Write("Count: ");
            int productCount = int.Parse(this.reader.Read());
            validator.ValidateLength(productCount, 1, 1000);

            this.writer.Write("Comment: ");
            string comment = this.reader.Read();
            comment = validator.ValidateValue(comment, false);
            //validator.ValidateLength(comment, 1, 300);

            var order = new OrderMakeModel()
            {
                Username = loggedUser,
                //ProductName = productName,
                //ProductCount = productCount,
                Comment = comment,
                OrderedOn = DateTime.Now
            };

            this.orderService.MakeOrder(order);

            return $"User {loggedUser} ordered {productCount} {productName} on {order.OrderedOn}";
        }
    }
}
