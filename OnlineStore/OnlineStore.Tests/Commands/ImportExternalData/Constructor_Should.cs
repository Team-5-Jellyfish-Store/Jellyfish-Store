using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Commands.AdminCommands;
using OnlineStore.Core.Contracts;
using OnlineStore.Logic.Contracts;

namespace OnlineStore.Tests.Commands.ImportExternalData
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnInstance_WhenProvidedValidParameters()
        {
            //Arrange
            var fakeUserSession = new Mock<IUserSession>();
            var fakeImportService = new Mock<IImportService>();

            //Act && Assert
            Assert.IsInstanceOfType(new ImportExternalDataCommand(fakeImportService.Object, fakeUserSession.Object), typeof(ImportExternalDataCommand));
        }

        [TestMethod]
        public void Throw_WhenUserSessionIsNull()
        {
            //Arrange
            var fakeImportService = new Mock<IImportService>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ImportExternalDataCommand(fakeImportService.Object, null ));

        }

        [TestMethod]
        public void Throw_WhenImportServiceIsNull()
        {
            //Arrange
            var fakeUserSession = new Mock<IUserSession>();


            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ImportExternalDataCommand(null, fakeUserSession.Object));
        }
    }
}
