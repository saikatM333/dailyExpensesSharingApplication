using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using modelLayer.model;
using repositoryLayer.Services;
using repositoryLayer.context;
using System.Threading;

namespace ExpenseSharingApp.Tests
{
    [TestClass]
    public class ExpenseServiceTests
    {
        private expensesDBContext _context;
        private ExpenseService _expenseService;
        private Mock<ILogger<ExpenseService>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<expensesDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new expensesDBContext(options);
            _mockLogger = new Mock<ILogger<ExpenseService>>();
            _expenseService = new ExpenseService(_context, _mockLogger.Object);
        }

        [TestMethod]
        public async Task AddExpenseAsync_ShouldAddEqualSplitExpense()
        {
            // Arrange
            var expenseDto = new CreateExpenseDto
            {
                Description = "Dinner",
                Amount = 3000,
                SplitMethod = "Equal",
                ExpenseParticipants = new List<ExpenseParticipantDto>
                {
                    new ExpenseParticipantDto { UserId = 1 },
                    new ExpenseParticipantDto { UserId = 2 },
                    new ExpenseParticipantDto { UserId = 3 }
                }
            };

            // Act
            var result = await _expenseService.AddExpenseAsync(expenseDto);

            // Assert
            Assert.AreEqual(1000, result.ExpenseParticipants.First().AmountOwed);
            Assert.AreEqual(1000, result.ExpenseParticipants.ElementAt(1).AmountOwed);
            Assert.AreEqual(1000, result.ExpenseParticipants.ElementAt(2).AmountOwed);
        }

        [TestMethod]
        public async Task AddExpenseAsync_ShouldAddExactSplitExpense()
        {
            // Arrange
            var expenseDto = new CreateExpenseDto
            {
                Description = "Shopping",
                Amount = 4299,
                SplitMethod = "Exact",
                ExpenseParticipants = new List<ExpenseParticipantDto>
                {
                    new ExpenseParticipantDto { UserId = 1, AmountOwed = 1500 },
                    new ExpenseParticipantDto { UserId = 2, AmountOwed = 799 },
                    new ExpenseParticipantDto { UserId = 3, AmountOwed = 2000 }
                }
            };

            // Act
            var result = await _expenseService.AddExpenseAsync(expenseDto);

            // Assert
            Assert.AreEqual(1500, result.ExpenseParticipants.First().AmountOwed);
            Assert.AreEqual(799, result.ExpenseParticipants.ElementAt(1).AmountOwed);
            Assert.AreEqual(2000, result.ExpenseParticipants.ElementAt(2).AmountOwed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AddExpenseAsync_ShouldThrowExceptionForNullDto()
        {
            // Arrange
            CreateExpenseDto expenseDto = null;

            // Act
            await _expenseService.AddExpenseAsync(expenseDto);
        }
    }
}
