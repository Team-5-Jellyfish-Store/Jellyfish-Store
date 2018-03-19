using OnlineStore.Models;

namespace OnlineStore.Core.Contracts
{
    public interface IUserSessionService
    {
        string GetLoggedUser();

        void SetLoggedUser(User user);

        void Logout();

        bool UserIsAdmin();

        bool UserIsModerator();
    }
}
