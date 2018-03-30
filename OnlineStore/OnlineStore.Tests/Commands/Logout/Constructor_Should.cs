using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineStore.Core.Commands;
using System;

namespace OnlineStore.Tests.Commands.Logout
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_UserSession_IsNull()
        {
            // Arrange
            Action creatingLogoutCmd = () => new LogoutCommand(null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(creatingLogoutCmd);
        }
    }
}
