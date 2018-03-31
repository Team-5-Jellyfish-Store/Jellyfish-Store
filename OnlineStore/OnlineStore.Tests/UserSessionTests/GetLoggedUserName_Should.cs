using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Providers.Providers;
using OnlineStore.DTO.UserModels.Contracts;
using System;

namespace OnlineStore.Tests.UserSessionTests
{
    [TestClass]
    public class GetLoggedUserName_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_LoggedUser_IsNull()
        {
            // Arrange
            var userSession = new UserSession();

            Action executingGetLoggedUserNameMethod = () => userSession.GetLoggedUserName();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingGetLoggedUserNameMethod);
        }

        [TestMethod]
        public void Return_LoggedUser_Username_LoggedUser_IsNotNull()
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
            var actualUsername = userSession.GetLoggedUserName();

            // Act & Assert
            Assert.AreEqual(fakeUsername, actualUsername);
        }
    }
}
