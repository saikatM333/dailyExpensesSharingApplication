using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using repositoryLayer.context;
using repositoryLayer.Services;
using Microsoft.EntityFrameworkCore;
using modelLayer.model;
using repositoryLayer.entity;
using System.Threading;

namespace ExpenseSharingApp.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private expensesDBContext _context;
        private UserService _userService;
        private Mock<ILogger<UserService>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<expensesDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new expensesDBContext(options);
            _mockLogger = new Mock<ILogger<UserService>>();
            _userService = new UserService(_context, _mockLogger.Object);
        }

        [TestMethod]
        public async Task CreateUserAsync_ShouldCreateUser()
        {
            // Arrange
            var userDto = new User
            {
                Email = "test@example.com",
                Name = "Test User",
                MobileNumber = "1234567890"
            };

            // Act
            var result = await _userService.CreateUserAsync(userDto);

            // Assert
            Assert.AreEqual(userDto.Email, result.Email);
            Assert.AreEqual(userDto.Name, result.Name);
            Assert.AreEqual(userDto.MobileNumber, result.MobileNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateUserAsync_ShouldThrowExceptionForNullDto()
        {
            // Arrange
            User userDto = null;

            // Act
            await _userService.CreateUserAsync(userDto);
        }
    }
}
