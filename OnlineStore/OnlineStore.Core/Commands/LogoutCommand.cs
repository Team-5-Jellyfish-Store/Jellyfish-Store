using OnlineStore.Core.Contracts;

namespace OnlineStore.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IUserSessionService sessionService;

        public LogoutCommand(IUserSessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        public string ExecuteThisCommand()
        {
            string currentUser = this.sessionService.GetLoggedUser();
            this.sessionService.Logout();
            return $"{currentUser} logged out successfully!";
        }
    }
}
