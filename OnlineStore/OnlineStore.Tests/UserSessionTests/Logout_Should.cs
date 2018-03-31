using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.UserSessionTests
{
    [TestClass]
    public class Logout_Should
    {
        [TestMethod]
        public void Set_LoggedUser_ToNull()
        {
            // Arrange
            var userSession = new MockUserSession();

            // Act
            userSession.Logout();
            var actualResult = userSession.ExposeLoggedUser();

            // Assert
            Assert.IsNull(actualResult);
        }
    }
}
