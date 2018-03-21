using OnlineStore.DTO;
using OnlineStore.Models;
using OnlineStore.Models.DataModels;

namespace OnlineStore.Core.Contracts
{
    public interface IUserSessionService
    {
        string GetLoggedUser();

        void SetLoggedUser(UserRegisterModel user);

        void Logout();

        bool UserIsAdmin();

        bool UserIsModerator();
    }
}
