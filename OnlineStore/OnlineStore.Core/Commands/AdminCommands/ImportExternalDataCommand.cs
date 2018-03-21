using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Core.Commands.AdminCommands
{
    public class ImportExternalDataCommand : ICommand
    {
        private readonly IUserSessionService sessionService;
        private readonly IImportService importService;

        public ImportExternalDataCommand(IImportService importService, IUserSessionService sessionService)
        {
            this.importService = importService;
            this.sessionService = sessionService;
        }

        public string ExecuteThisCommand()
        {
            var result = this.importService.Import();
            
            return result;
        }
    }
}