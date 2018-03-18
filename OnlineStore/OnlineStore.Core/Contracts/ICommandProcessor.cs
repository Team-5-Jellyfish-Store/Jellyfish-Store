namespace OnlineStore.Core.Contracts
{
    public interface ICommandProcessor
    {
        string ProcessSingleCommand(ICommand command);
    }
}
