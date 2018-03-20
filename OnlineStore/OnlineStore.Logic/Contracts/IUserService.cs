namespace OnlineStore.Logic.Contracts
{
    public interface IUserService
    {
        void RegisterUser(string username, string password, string email, string firstName, string lastName, string addressText);
    }
}
