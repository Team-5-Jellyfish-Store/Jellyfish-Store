using System;
using OnlineStore.Core.Contracts;
using System.IO;

namespace OnlineStore.Core.Providers.Providers
{
    public class FileReader : IFileReader
    {
        public string ReadAllText(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException();
            }

            return File.ReadAllText(filePath);
        }
    }
}
