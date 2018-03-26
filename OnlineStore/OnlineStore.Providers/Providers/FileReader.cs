using OnlineStore.Core.Contracts;
using System.IO;

namespace OnlineStore.Core.Providers.Providers
{
    public class FileReader : IFileReader
    {
        public string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
