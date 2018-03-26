using System;
using Autofac;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IComponentContext container;

        public CommandFactory(IComponentContext container)
        {
            this.container = container ?? throw new ArgumentNullException();
        }

        protected IComponentContext Container => container;

        public ICommand CreateCommand(string commandName)
        {
            if (string.IsNullOrWhiteSpace(commandName))
            {
                throw new ArgumentNullException("commandName is null");
            }
            return this.container.ResolveNamed<ICommand>(commandName);
        }
    }
}
