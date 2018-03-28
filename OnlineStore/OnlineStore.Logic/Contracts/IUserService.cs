using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;

namespace OnlineStore.Logic.Contracts
{
    public interface IUserService
    {
        void RegisterUser(IUserRegisterModel userRegisterModel);

        IUserLoginModel GetRegisteredUser(string userName);
    }
}
