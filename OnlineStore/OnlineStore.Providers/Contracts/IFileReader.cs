namespace OnlineStore.Core.Contracts
{
    public interface IFileReader
    {
        string ReadAllText(string filePath);
    }
}
