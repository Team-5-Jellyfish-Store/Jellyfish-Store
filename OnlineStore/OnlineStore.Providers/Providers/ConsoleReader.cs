﻿using System;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Providers.Providers
{
    public class ConsoleReader : IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
