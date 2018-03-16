using System;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Factories
{
    public class CommandParser : ICommandParser
    {
        private readonly ICommandFactory cmdFactory;

        public CommandParser(ICommandFactory cmdFactory)
        {
            if (cmdFactory == null)
            {
                throw new ArgumentNullException("cmdFactory");
            }
            this.cmdFactory = cmdFactory;
        }

        protected ICommandFactory CmdFactory => cmdFactory;

        public ICommand ParseCommand(string fullCommandName)
        {
            var commandParametersSplit = fullCommandName.Trim().Split(new [] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            var commandName = commandParametersSplit[0];

            var command = this.CmdFactory.CreateCommand(commandName);

            return command;
        }
    }
}
