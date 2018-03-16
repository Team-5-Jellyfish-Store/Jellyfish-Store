namespace OnlineStore.Core.Contracts
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string commandName);
    }
}
