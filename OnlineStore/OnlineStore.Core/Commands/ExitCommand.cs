using System;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string ExecuteThisCommand(string[] commandParameters)
        {
            //Environment.Exit(0);
            return "Goodbye! Thank you for your business!";
        }
    }
}
