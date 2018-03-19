using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Models;
using OnlineStore.Models.DataModels;
using OnlineStore.Models.Enums;

namespace OnlineStore.Core.UserService
{
    public class UserSessionService : IUserSessionService
    {
        private User user;

        public string GetLoggedUser()
        {
            if (this.user == null)
            {
                throw new ArgumentNullException("No logged user!");
            }

            return this.user.Username;
        }

        public void SetLoggedUser(User user)
        {
            this.user = user ?? throw new ArgumentNullException();
        }

        public void Logout() => this.user = null;

        public bool UserIsAdmin()
        {
            return this.user != null && this.user.Role == UserRole.Admin;
        }

        public bool UserIsModerator()
        {
            return this.user != null && this.user.Role == UserRole.Moderator;
        }
    }
}
