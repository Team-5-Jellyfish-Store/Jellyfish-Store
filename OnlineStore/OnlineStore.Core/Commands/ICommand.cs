namespace OnlineStore.Core.Commands
{
    public interface ICommand
    {
        void Execute(string[] parameters);
    }
}
