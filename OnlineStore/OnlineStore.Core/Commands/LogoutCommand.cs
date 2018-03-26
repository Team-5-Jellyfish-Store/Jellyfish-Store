using OnlineStore.Core.Contracts;
using System;

namespace OnlineStore.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly string UserLoggedOutSuccessMessage = "User {0} logged out successfully!";
        private readonly string NoLoggedUserFailMessage = "No logged user!";

        private readonly IUserSession userSession;

        public LogoutCommand(IUserSession userSession)
        {
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
        }

        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasSomeoneLogged())
            {
                throw new ArgumentException(this.NoLoggedUserFailMessage);
            }

            var userToLogout = this.userSession.GetLoggedUserName();

            this.userSession.Logout();

            return string.Format(this.UserLoggedOutSuccessMessage, userToLogout);
        }
    }
}
