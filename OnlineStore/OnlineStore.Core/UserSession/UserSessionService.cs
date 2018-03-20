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
            this.CheckForLoggedUser();

            return this.user.Username;
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

        private void CheckForLoggedUser()
        {
            if (this.user == null)
            {
                throw new ArgumentException("No logged user!");
            }
        }
    }
}
