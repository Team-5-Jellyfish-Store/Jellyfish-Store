using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Models;
using OnlineStore.Models.DataModels;
using OnlineStore.Models.Enums;

namespace OnlineStore.Core.UserService
{
    public class UserSessionService : IUserSessionService
    {
        public User User { get; private set; }
        public void SetLoggedUser(User user)
        {
            this.User = user ?? throw new ArgumentNullException();
        }

        public void Logout() => this.User = null;

        public bool UserIsAdmin()
        {
            return this.User != null && this.User.Role == UserRole.Admin;
        }

        public bool UserIsModerator()
        {
            return this.User != null && this.User.Role == UserRole.Moderator;
        }
    }
}
