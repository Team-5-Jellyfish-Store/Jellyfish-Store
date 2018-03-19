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
            var inputLine = this.reader.Read();

            while (true)
            {
                try
                {
                    var command = this.commandParser.ParseCommand(inputLine);
                    var result = this.commandProcessor.ProcessSingleCommand(command);
                    this.writer.WriteLine(result);
                }
                catch (NotSupportedException e) { this.writer.WriteLine(e.Message); }
                catch (InvalidOperationException e) { this.writer.WriteLine(e.Message); }
                catch (ArgumentException e) { this.writer.WriteLine(e.Message); }
                catch (ComponentNotRegisteredException) { this.writer.WriteLine($"There is no command named [{inputLine}] implemented! Please contact Dev team to implement it :)"); }

                this.writer.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");

                inputLine = this.reader.Read();
            }
        }
    }
}