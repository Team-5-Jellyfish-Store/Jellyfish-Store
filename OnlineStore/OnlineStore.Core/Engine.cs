﻿using System;
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
        private readonly IFileReader fileReader;


        public Engine
            (
            ICommandParser commandParser,
            ICommandProcessor commandProcessor,
            IWriter writer,
            IReader reader,
            IFileReader fileReader
            )
        {
            this.commandParser = commandParser ?? throw new ArgumentNullException();
            this.commandProcessor = commandProcessor ?? throw new ArgumentNullException();
            this.writer = writer ?? throw new ArgumentNullException();
            this.reader = reader ?? throw new ArgumentNullException();
            this.fileReader = fileReader ?? throw new ArgumentNullException();
        }

        protected ICommandParser CommandParser => this.commandParser;
        protected ICommandProcessor CommandProcessor => this.commandProcessor;
        protected IWriter Writer => this.writer;
        protected IReader Reader => this.reader;
        protected IFileReader FileReader => this.fileReader;

        public void Run()
        {
            string welcomeText = this.fileReader.ReadAllText("../../../Datasets/WellcomeText.txt");
            this.writer.WriteLine(welcomeText);

            while (true)
            {
                this.writer.Write("Please enter command name: ");
                var inputLine = this.reader.Read().ToLower().Trim();

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
                    this.writer.WriteLine($"There is no command named [{inputLine}] implemented! Please type help to see the list of available commands and if not available contact Dev team to implement it :)");
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
