using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Providers.Providers;
using OnlineStore.DTO.UserModels.Contracts;

namespace OnlineStore.Tests.UserSessionTests
{
    [TestClass]
    public class HasSomeoneLogged_Should
    {
        [TestMethod]
        public void Return_False_When_LoggedUser_IsNull()
        {
            // Arrange
            var userSession = new UserSession();

            // Act
            var actualResult = userSession.HasSomeoneLogged();

            // Act & Assert
            Assert.IsFalse(actualResult);
        }

        [TestMethod]
        public void Return_True_When_LoggedUser_IsNotNull()
        {
            // Arrange
            var fakeUsername = "testUsername";

            var fakeUserLoginModel = new Mock<IUserLoginModel>();

            var userSession = new UserSession();

            fakeUserLoginModel
                .SetupGet(ulm => ulm.Username)
                .Returns(fakeUsername);

            userSession.Login(fakeUserLoginModel.Object);

            // Act
            var actualResult = userSession.HasSomeoneLogged();

            // Act & Assert
            Assert.IsTrue(actualResult);
        }
    }
}
