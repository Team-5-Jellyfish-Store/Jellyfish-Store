using System;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class AddProductToProductsCommand : ICommand
    {
        public string ExecuteThisCommand()
        {
            Console.WriteLine("I added the file...");
            return "not implemented";
        }
    }
}
