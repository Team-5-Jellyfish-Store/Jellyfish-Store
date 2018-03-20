using System;
using OnlineStore.Core.Contracts;
using OnlineStore.Models;
using OnlineStore.Models.DataModels;
using OnlineStore.Models.Enums;

namespace OnlineStore.Core.UserSession
{
    public class UserSessionService : IUserSessionService
    {
        private User user;

        public string GetLoggedUser()
        {
            if (this.CheckForLoggedUser())
            {
                return this.user.Username;
            }

            return null;
        }

        public void SetLoggedUser(User user)
        {
            this.user = user ?? throw new ArgumentNullException();
        }

        public void Logout() => this.user = null;

        public bool UserIsAdmin()
        {
            this.CheckForLoggedUser();

            return this.user != null && this.user.Role == UserRole.Admin;
        }

        public bool UserIsModerator()
        {
            this.CheckForLoggedUser();

            return this.user != null && this.user.Role == UserRole.Moderator;
        }

        private bool CheckForLoggedUser()
        {
            if (this.user == null)
            {
                return false;
            }

            return true;
        }
    }
}
