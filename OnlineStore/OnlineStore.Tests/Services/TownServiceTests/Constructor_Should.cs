using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineStore.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Tests.Services.TownServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_WhenContextIsNull()
        {
            //arrange&act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new TownService(null));
        }
    }
}
