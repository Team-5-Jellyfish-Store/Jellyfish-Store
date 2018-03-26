using OnlineStore.Core.Contracts;
using OnlineStore.Core.Factories;

namespace OnlineStore.Tests.Mocks
{
    public class MockCommandParser : CommandParser
    {
        public ICommandFactory ExposedCommandFactory => this.CmdFactory;

        public MockCommandParser(ICommandFactory cmdFactory) : base(cmdFactory)
        {
        }
    }
}
