﻿using OnlineStore.Core;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Tests.Mocks
{
    public class MockEngine : Engine
    {
        public ICommandParser ExposedCommandParser => this.CommandParser;
        public ICommandProcessor ExposedCommandProcessor => this.CommandProcessor;
        public IWriter ExposedWriter => this.Writer;
        public IReader ExposedReader => this.Reader;


        public MockEngine(ICommandParser commandParser, ICommandProcessor commandProcessor, IWriter writer, IReader reader) : base(commandParser, commandProcessor, writer, reader)
        {
        }
    }
}
