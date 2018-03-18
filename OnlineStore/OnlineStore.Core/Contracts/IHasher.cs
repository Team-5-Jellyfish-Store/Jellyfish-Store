namespace OnlineStore.Core.Contracts
{
    public interface IHasher
    {
        string CreatePassword(string password);

        bool CheckPassword(string enteredPassword, string actualPassword);
    }
}
