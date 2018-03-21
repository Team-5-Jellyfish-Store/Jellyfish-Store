using OnlineStore.DTO;

namespace OnlineStore.Logic.Contracts
{
    public interface IUserService
    {
        void RegisterUser(UserRegisterModel userRegisterModel);
        UserRegisterModel GetUserWithUserName(string userName);
    }
}
