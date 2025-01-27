using Microsoft.VisualStudio.TestTools.UnitTesting;
using HardkorowyKodsuApi.Repositories;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HardkorowyKodsuApi.Models;
using System.Data;

namespace HardkorowyKodsuApi.Controllers.Tests
{
    [TestClass]
    public class DbSchemaControllerTests
    {
        private Mock<DbRepositorySQLSERVER> _mockRepository;
        private DbSchemaController _controller;

        [TestInitialize]
        public void Setup()
        {

            var mockDbConnection = new Mock<IDbConnection>();
            _mockRepository = new Mock<DbRepositorySQLSERVER>(mockDbConnection.Object);
            _controller = new DbSchemaController(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetAllTablesAndViews_ShouldReturnOkWithTablesAndViews()
        {
            // Arrange
            var mockTablesAndViews = new List<string> { "dbo.Users", "dbo.Orders" };
            _mockRepository
                .Setup(repo => repo.GetAllTablesAndViewsAsync())
                .ReturnsAsync(mockTablesAndViews);

            // Act
            var result = await _controller.GetAllTablesAndViews();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var data = okResult.Value as IEnumerable<string>;
            Assert.IsNotNull(data);
            CollectionAssert.AreEqual(mockTablesAndViews, new List<string>(data));
        }

        [TestMethod]
        public async Task GetAllTablesAndViews_ShouldReturnInternalServerErrorOnException()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetAllTablesAndViewsAsync())
                .ThrowsAsync(new System.Exception("Database error"));

            // Act
            var result = await _controller.GetAllTablesAndViews();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var errorResult = result as ObjectResult;
            Assert.IsNotNull(errorResult);
            Assert.AreEqual(500, errorResult.StatusCode);
            Assert.IsTrue(errorResult.Value.ToString().Contains("Database error"));
        }

        [TestMethod]
        public async Task GetTableOrViewStructure_ValidTable_ShouldReturnOkWithStructure()
        {
            // Arrange
            var mockStructure = new List<string>
            {
                 "Id", "Name"
            };
            _mockRepository
                .Setup(repo => repo.GetTableOrViewStructureAsync("dbo.Users"))
                .ReturnsAsync(mockStructure);

            // Act
            var result = await _controller.GetTableOrViewStructure("dbo.Users");

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var data = okResult.Value as IEnumerable<string>;
            Assert.IsNotNull(data);
            CollectionAssert.AreEqual(mockStructure, new List<string>(data));
        }

        [TestMethod]
        public async Task GetTableOrViewStructure_InvalidTable_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetTableOrViewStructureAsync("dbo.NonExistent"))
                .ReturnsAsync((IEnumerable<string>)null);

            // Act
            var result = await _controller.GetTableOrViewStructure("dbo.NonExistent");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public async Task GetTableOrViewStructure_ShouldReturnBadRequestForEmptyTableName()
        {
            // Act
            var result = await _controller.GetTableOrViewStructure("");

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.IsTrue(badRequestResult.Value.ToString().Contains("Table name is required"));
        }
    }
}
