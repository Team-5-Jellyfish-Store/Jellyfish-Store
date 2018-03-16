namespace OnlineStore.Core.Contracts
{
    public interface ICommand
    {
        string ExecuteThisCommand(string[] commandParameters);
    }
}
