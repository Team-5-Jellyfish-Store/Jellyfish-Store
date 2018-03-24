using OnlineStore.Core.Contracts;
using System;

namespace OnlineStore.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IUserSession userSession;

        public LogoutCommand(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasSomeoneLogged())
            {
                throw new ArgumentException("No logged user!");
            }

            this.userSession.Logout();

            return $"User logged out successfully!";
        }
    }
}
