using OnlineStore.Core.Contracts;
using OnlineStore.DTO.UserModels;
using OnlineStore.Models.Enums;
using System;

namespace OnlineStore.Core.Providers.Providers
{
    public class UserSession : IUserSession
    {
        private UserLoginModel loggedUser;

        public string GetLoggedUserName()
        {
            if (this.loggedUser == null)
            {
                throw new ArgumentException("No user logged in!");
            }

            return this.loggedUser.Username;
        }

        public bool HasAdminRights()
        {
            if (this.loggedUser == null)
            {
                throw new ArgumentException("No user logged in!");
            }

            return this.loggedUser.Role == UserRole.Admin || this.loggedUser.Role == UserRole.Moderator;
        }

        public bool HasSomeoneLogged()
        {
            return this.loggedUser != null;
        }

        public void Login(UserLoginModel userToLogin)
        {
            if (this.loggedUser != null)
            {
                throw new ArgumentException("User already logged in!");
            }

            if (userToLogin == null)
            {
                throw new ArgumentNullException(nameof(userToLogin));
            }

            this.loggedUser = userToLogin;
        }

        public void Logout()
        {
            this.loggedUser = null;
        }
    }
}
