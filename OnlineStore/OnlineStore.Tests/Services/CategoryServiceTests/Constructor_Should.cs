using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Data.Contracts;
using OnlineStore.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests.Services.CategoryServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //Arrange
            var stubMapper = new Mock<IMapper>();
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CategoryService(null, stubMapper.Object));

        }

        [TestMethod]
        public void Throw_WhenMapperIsNull()
        {
            //Arrange
            var stubDBContext = new Mock<IOnlineStoreContext>();
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CategoryService(stubDBContext.Object, null));

        }
    }
}
