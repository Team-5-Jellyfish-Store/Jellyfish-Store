﻿using System;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Providers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
