using OnlineStore.DTO.UserModels;

namespace OnlineStore.Logic.Contracts
{
    public interface IUserService
    {
        void RegisterUser(UserRegisterModel userRegisterModel);

        UserLoginModel GetRegisteredUser(string userName);
    }
}
