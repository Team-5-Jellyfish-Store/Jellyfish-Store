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

        protected ICommandFactory CmdFactory => this.cmdFactory;

        public ICommand ParseCommand(string commandName)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                throw new ArgumentNullException("Null string in command parser!");
            }
            var command = this.CmdFactory.CreateCommand(commandName);

            return command;
        }
    }
}
