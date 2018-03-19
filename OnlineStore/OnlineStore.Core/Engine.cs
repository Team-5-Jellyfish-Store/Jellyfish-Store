using System;
using Autofac.Core.Registration;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core
{
    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly ICommandParser commandParser;
        private readonly ICommandProcessor commandProcessor;

        public Engine
            (
            ICommandParser commandParser,
            ICommandProcessor commandProcessor,
            IWriter writer,
            IReader reader
            )
        {
            this.commandParser = commandParser ?? throw new ArgumentNullException();
            this.commandProcessor = commandProcessor ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
            this.reader = reader ?? throw new ArgumentNullException();
        }

        public void Run()
        {
            this.writer.WriteLine(System.IO.File.ReadAllText("../../../Datasets/WellcomeText.txt"));

            while (true)
            {
                this.writer.Write("Please enter command name: ");
                var inputLine = this.reader.Read();
                try
                {
                    var command = this.commandParser.ParseCommand(inputLine);
                    var result = this.commandProcessor.ProcessSingleCommand(command);
                    this.writer.WriteLine(result);
                }
                catch (ComponentNotRegisteredException)
                {
                    this.writer.WriteLine($"There is no command named [{inputLine}] implemented! Please contact Dev team to implement it :)");
                }
                catch (Exception e)
                {
                    while (e.InnerException != null)
                    {
                        e = e.InnerException;
                    }
                    this.writer.WriteLine(e.Message);
                }

                this.writer.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");
            }
        }
    }
}
