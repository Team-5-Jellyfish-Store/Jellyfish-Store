﻿using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class ImportExternalDataCommand : ICommand
    {
        private readonly IUserSession userSession;
        private readonly IImportService importService;

        public ImportExternalDataCommand(IImportService importService, IUserSession userSession)
        {
            this.importService = importService;
            this.userSession = userSession;
        }

        public string ExecuteThisCommand()
        {
            if (!this.userSession.HasSomeoneLogged())
            {
                return "Login First!";
            }

            if (!this.userSession.HasAdminRights())
            {
                return "User must be admin or moderator in order to import data!";
            }

            var result = this.importService.Import();

            return result;
        }
    }
}