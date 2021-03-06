﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlineStore.Core.Contracts;
using OnlineStore.DTO.ProductModels;
using OnlineStore.DTO.ProductModels.Contracts;
using OnlineStore.Logic.Contracts;
using OnlineStore.Providers.Contracts;
using OnlineStore.Tests.Mocks;

namespace OnlineStore.Tests.Services.ImportService
{
    [TestClass]
    public class ImportProducts_Should
    {
        [TestMethod]
        public void InvokeFileRead_ToReadImportString()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            var mockFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, mockFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportProductsFunction();

            //Assert
            mockFileReader.Verify(v => v.ReadAllText(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void InvokeJsonread_ToDeserializeImportString()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var mockJsonService = new Mock<IJsonService>();
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, mockJsonService.Object);
            //Act
            importService.ExposedImportProductsFunction();

            //Assert
            mockJsonService.Verify(v => v.DeserializeProducts(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void InvokeValidator_ToValidateObject()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeProducts(It.IsAny<string>())).Returns(new[] { new ProductImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var mockValidator = new Mock<IValidator>();
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, mockValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportProductsFunction();

            //Assert
            mockValidator.Verify(v => v.IsValid(It.IsAny<object>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ReturnInvalidInputMessage_WhenModelIsInvalid()
        {
            //Arrange
            var fakeProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeProducts(It.IsAny<string>())).Returns(new[] { new ProductImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(false);
            var importService = new MockImportService(fakeProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, mockValidator.Object, fakeJsonService.Object);
            string expectedMessage = "Import rejected. Input is with invalid format.\r\n";
            //Act
            string actualMessage = importService.ExposedImportProductsFunction();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InvokeProductExistsByName_WhenModelIsValid()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            fakeJsonService.Setup(s => s.DeserializeProducts(It.IsAny<string>())).Returns(new[] { new ProductImportModel() });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(mockProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportProductsFunction();

            //Assert
            mockProductService.Verify(v => v.ProductExistsByName(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ReturnsCorrectMessage_WhenModelIsValidAndProductExists()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductExistsByName(It.IsAny<string>())).Returns(true);
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            var fakeProduct = new ProductImportModel
            {
                Name = "test"
            };
            fakeJsonService.Setup(s => s.DeserializeProducts(It.IsAny<string>())).Returns(new[] { fakeProduct });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(mockProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            string expectedMessage = $"Product {fakeProduct.Name} already exists!\r\n";
            //Act
            string actualMessage = importService.ExposedImportProductsFunction();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void AddsTheProductToListOfValidProducts_WhenModelIsValidAndProductDoesNotExist()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductExistsByName(It.IsAny<string>())).Returns(false);
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            var fakeProduct = new ProductImportModel
            {
                Name = "test"
            };
            fakeJsonService.Setup(s => s.DeserializeProducts(It.IsAny<string>())).Returns(new[] { fakeProduct });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(mockProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);

            //Act
            importService.ExposedImportProductsFunction();

            //Assert
            Assert.IsTrue(importService.ExposedValidProducts.Any(a => a.Name == fakeProduct.Name));
        }

        [TestMethod]
        public void ReturnsValidMessage_WhenProductIsAdded()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductExistsByName(It.IsAny<string>())).Returns(false);
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            var fakeProduct = new ProductImportModel
            {
                Name = "test",
                Quantity = 5
            };
            fakeJsonService.Setup(s => s.DeserializeProducts(It.IsAny<string>())).Returns(new[] { fakeProduct });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(mockProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            string expectedMessage = $"{fakeProduct.Quantity} items of product {fakeProduct.Name} added successfully!\r\n";
            //Act
            string actualMessage = importService.ExposedImportProductsFunction();

            //Assert

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void InvokeAddProductRange_WhenAllProductsAreValidated()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductExistsByName(It.IsAny<string>())).Returns(false);
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            var fakeProduct = new ProductImportModel();
            fakeJsonService.Setup(s => s.DeserializeProducts(It.IsAny<string>())).Returns(new[] { fakeProduct });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.Setup(s => s.IsValid(It.IsAny<object>())).Returns(true);
            var importService = new MockImportService(mockProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            //Act
            importService.ExposedImportProductsFunction();

            //Assert
            mockProductService.Verify(v => v.AddProductRange(It.IsAny<IList<IProductImportModel>>()), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectMessage_WhenMethodReturns()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.ProductExistsByName(It.IsAny<string>())).Returns(false);
            var fakeCourierService = new Mock<ICourierService>();
            var fakeSupplierService = new Mock<ISupplierService>();
            var fakeJsonService = new Mock<IJsonService>();
            var fakeProduct1 = new ProductImportModel
            {
                Name = "test",
                Quantity = 1
            };
            var fakeProduct2 = new ProductImportModel ();
            fakeJsonService.Setup(s => s.DeserializeProducts(It.IsAny<string>())).Returns(new[] { fakeProduct1, fakeProduct2 });
            var fakeFileReader = new Mock<IFileReader>();
            var fakeValidator = new Mock<IValidator>();
            fakeValidator.SetupSequence(s => s.IsValid(It.IsAny<object>())).Returns(true).Returns(false);
            var importService = new MockImportService(mockProductService.Object, fakeCourierService.Object,
                fakeSupplierService.Object, fakeFileReader.Object, fakeValidator.Object, fakeJsonService.Object);
            string expectedMessage =
                $"{fakeProduct1.Quantity} items of product {fakeProduct1.Name} added successfully!\r\n" +
                "Import rejected. Input is with invalid format.\r\n";
            //Act
            string actualMessage = importService.ExposedImportProductsFunction();

            //Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}
