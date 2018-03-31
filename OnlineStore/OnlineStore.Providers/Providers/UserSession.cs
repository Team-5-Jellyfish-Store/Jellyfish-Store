using OnlineStore.Core.Contracts;
using OnlineStore.DTO.UserModels;
using OnlineStore.DTO.UserModels.Contracts;
using OnlineStore.Models.Enums;
using System;

namespace OnlineStore.Core.Providers.Providers
{
    public class UserSession : IUserSession
    {
        protected virtual IUserLoginModel LoggedUser { get; set; }

        public string GetLoggedUserName()
        {
            if (this.LoggedUser == null)
            {
                throw new ArgumentException("No user logged in!");
            }

            return this.LoggedUser.Username;
        }

        public bool HasAdminRights()
        {
            if (this.LoggedUser == null)
            {
                throw new ArgumentException("No user logged in!");
            }

            return this.LoggedUser.Role == UserRole.Admin || this.LoggedUser.Role == UserRole.Moderator;
        }

        public bool HasSomeoneLogged()
        {
            return this.LoggedUser != null;
        }

        public void Login(IUserLoginModel userToLogin)
        {
            if (this.LoggedUser != null)
            {
                throw new ArgumentException("User already logged in!");
            }

            if (userToLogin == null)
            {
                throw new ArgumentNullException(nameof(userToLogin));
            }

            this.LoggedUser = userToLogin;
        }

        public void Logout()
        {
            this.LoggedUser = null;
        }
    }
}
