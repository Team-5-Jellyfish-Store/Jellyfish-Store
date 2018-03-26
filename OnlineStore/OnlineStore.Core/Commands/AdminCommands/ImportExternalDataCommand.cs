using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;
using System;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class ImportExternalDataCommand : ICommand
    {
        private readonly string NoLoggedUserFailMessage = "Login first!";
        private readonly string UserHasNoRightsFailMessage = "User is neither admin nor moderator and cannot add products!";

        private readonly IUserSession userSession;
        private readonly IImportService importService;

        public ImportExternalDataCommand(IImportService importService, IUserSession userSession)
        {
            this.importService = importService ?? throw new ArgumentNullException(nameof(importService));
            this.userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
        }

        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasSomeoneLogged())
            {
                return this.NoLoggedUserFailMessage;
            }

            if (!this.userSession.HasAdminRights())
            {
                return this.UserHasNoRightsFailMessage;
            }

            var result = this.importService.Import();

            return result;
        }
    }
}