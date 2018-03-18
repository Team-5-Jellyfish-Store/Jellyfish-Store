using System;
using System.Linq;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Factories
{
    public class CommandProcessor : ICommandProcessor
    {
        public string ProcessSingleCommand(ICommand command)
        {
            var result = command.ExecuteThisCommand();
            return result;
        }
    }
}
