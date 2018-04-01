using System;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Commands
{
    public class HelpCommand : ICommand
    {
        private readonly IFileReader fileReader;

        public HelpCommand(IFileReader fileReader)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        public string ExecuteThisCommand()
        {
            var helpText = this.fileReader.ReadAllText("../../../Datasets/HelpText.txt");
            return helpText;
        }
    }
}
