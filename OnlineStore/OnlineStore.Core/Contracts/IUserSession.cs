using OnlineStore.DTO.UserModels;

namespace OnlineStore.Core.Contracts
{
    public interface IUserSession
    {
        void Login(UserLoginModel username);

        void Logout();

        bool HasSomeoneLogged();

        bool HasAdminRights();

        string GetLoggedUserName();
    }
}
