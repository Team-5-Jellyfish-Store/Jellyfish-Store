using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;

namespace OnlineStore.Core.Contracts
{
    public interface IUserSession
    {
        void Login(IUserLoginModel username);

        void Logout();

        bool HasSomeoneLogged();

        bool HasAdminRights();

        string GetLoggedUserName();
    }
}
