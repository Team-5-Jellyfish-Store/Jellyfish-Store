namespace OnlineStore.Core.Security
{
    public interface IHasher
    {
        string CreatePassword(string password);

        bool CheckPassword(string enteredPassword, string actualPassword);
    }
}
