using System;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Factories
{
    public class CommandProcessor : ICommandProcessor
    {
        public string ProcessSingleCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("Command is null in commandProcessor");
            }
            var result = command.ExecuteThisCommand();
            return result;
        }
    }
}
