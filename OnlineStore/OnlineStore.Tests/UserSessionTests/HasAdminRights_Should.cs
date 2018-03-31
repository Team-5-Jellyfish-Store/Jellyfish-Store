using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Providers.Providers;
using OnlineStore.DTO.UserModels.Contracts;
using OnlineStore.Models.Enums;
using System;

namespace OnlineStore.Tests.UserSessionTests
{
    [TestClass]
    public class HasAdminRights_Should
    {
        [TestMethod]
        public void Throw_ArgumentException_When_LoggedUser_IsNull()
        {
            // Arrange
            var userSession = new UserSession();

            Action executingGetLoggedUserNameMethod = () => userSession.HasAdminRights();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(executingGetLoggedUserNameMethod);
        }

        [TestMethod]
        public void Return_True_When_LoggedUserRole_Is_AdminOrModerator()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeRole = UserRole.Admin;

            var fakeUserLoginModel = new Mock<IUserLoginModel>();

            var userSession = new UserSession();

            fakeUserLoginModel
                .SetupGet(ulm => ulm.Username)
                .Returns(fakeUsername);
            
            fakeUserLoginModel
                .SetupGet(ulm => ulm.Role)
                .Returns(fakeRole);

            userSession.Login(fakeUserLoginModel.Object);

            // Act
            var actualResult = userSession.HasAdminRights();

            // Act & Assert
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void Return_False_When_LoggedUserRole_IsNot_AdminOrModerator()
        {
            // Arrange
            var fakeUsername = "testUsername";
            var fakeRole = UserRole.Client;

            var fakeUserLoginModel = new Mock<IUserLoginModel>();

            var userSession = new UserSession();

            fakeUserLoginModel
                .SetupGet(ulm => ulm.Username)
                .Returns(fakeUsername);
            
            fakeUserLoginModel
                .SetupGet(ulm => ulm.Role)
                .Returns(fakeRole);

            userSession.Login(fakeUserLoginModel.Object);

            // Act
            var actualResult = userSession.HasAdminRights();

            // Act & Assert
            Assert.IsFalse(actualResult);
        }
    }
}
