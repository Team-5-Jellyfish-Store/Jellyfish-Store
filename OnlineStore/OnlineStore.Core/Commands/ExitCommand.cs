using System;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Commands
{
    public class ExitCommand : ICommand
    {
        private readonly IWriter writer;

        public ExitCommand(IWriter writer)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public string ExecuteThisCommand()
        {
            this.writer.WriteLine("Goodbye! Thank you for your business!");
            Environment.Exit(0);
            return string.Empty;
        }
    }
}
