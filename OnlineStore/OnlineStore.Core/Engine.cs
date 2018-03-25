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

        protected ICommandParser CommandParser => this.commandParser;
        protected ICommandProcessor CommandProcessor => this.commandProcessor;
        protected IWriter Writer => this.writer;
        protected IReader Reader => this.reader;

        public void Run()
        {
            this.writer.WriteLine(System.IO.File.ReadAllText("../../../Datasets/WellcomeText.txt"));

            while (true)
            {
                this.writer.Write("Please enter command name: ");
                var inputLine = this.reader.Read();

                if (inputLine == "exit")
                {
                    this.writer.WriteLine("Goodbye! Thank you for your business!");
                    return;
                }

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
                ////below should be used only in debug mode! Comment the upper paragraph and uncomment the below para.
                //this.writer.Write("Please enter command name: ");
                //var inputLine = this.reader.Read();

                //var command = this.commandParser.ParseCommand(inputLine);
                //var result = this.commandProcessor.ProcessSingleCommand(command);
                //this.writer.WriteLine(result);

                //this.writer.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");
            }
        }
    }
}
