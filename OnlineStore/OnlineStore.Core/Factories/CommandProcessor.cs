using System;
using System.Linq;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Factories
{
    public class CommandProcessor : ICommandProcessor
    {
        public string ProcessSingleCommand(ICommand command, string fullCommandParams)
        {
            var lineParameters = fullCommandParams.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            var result = command.ExecuteThisCommand(lineParameters.Skip(1).ToArray());
            return result;
        }
    }
}
