using modelLayer.model;
using repositoryLayer.context;
using repositoryLayer.entity;
using repositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace repositoryLayer.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly expensesDBContext _context;
        private readonly ILogger<ExpenseService> _logger;
        public ExpenseService(expensesDBContext context , ILogger<ExpenseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Expense> AddExpenseAsync(CreateExpenseDto expenseDto)
        {
            if (expenseDto == null) throw new ArgumentNullException(nameof(expenseDto));
            var expense = new Expense
            {
                Description = expenseDto.Description,
                TotalAmount = expenseDto.Amount,
                SplitMethod = expenseDto.SplitMethod,
                ExpenseParticipants = expenseDto.ExpenseParticipants.Select(p => new ExpenseParticipant
                {
                    UserId = p.UserId,
                    AmountOwed = p.AmountOwed,
                    PercentageOwed = expenseDto.SplitMethod == "Percentage" ? p.AmountOwed : 0
                }).ToList()
            };
            try
            {
                if (expense.SplitMethod == "Equal")
                {
                    var participantCount = expense.ExpenseParticipants.Count;
                    var splitAmount = expense.TotalAmount / participantCount;

                    foreach (var participant in expense.ExpenseParticipants)
                    {
                        participant.AmountOwed = splitAmount;
                    }
                }
                else if (expense.SplitMethod == "Percentage")
                {
                    var totalPercentage = expense.ExpenseParticipants.Sum(p => p.PercentageOwed);
                    if (totalPercentage != 100)
                    {
                        throw new ArgumentException("Percentages must add up to 100.");
                    }
                    else
                    {

                        foreach (var participant in expense.ExpenseParticipants)
                        {
                            participant.AmountOwed = expense.TotalAmount * participant.PercentageOwed / 100;
                        }
                        
                    }
                }
                _context.Expenses.Add(expense);
                await _context.SaveChangesAsync();
                return expense;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the expense.");
                throw new ApplicationException("An error occurred while adding the expense.");
            }
        }

        public async Task<IEnumerable<Expense>> GetUserExpensesAsync(int userId)
        {
            try
            {
                return await _context.Expenses
                    .Include(e => e.ExpenseParticipants)
                    .Where(e => e.ExpenseParticipants.Any(ep => ep.UserId == userId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the expense.");
                throw new ApplicationException("An error occurred while adding the expense.");
            }
        }

        public async Task<IEnumerable<Expense>> GetOverallExpensesAsync()
        {
            try
            {
                return await _context.Expenses
                    .Include(e => e.ExpenseParticipants)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the expense.");
                throw new ApplicationException("An error occurred while adding the expense.");
            }
        }

        public async Task<byte[]> GetBalanceSheetAsync()
        {
            var expenses = await GetOverallExpensesAsync();
            var csv = new StringBuilder();

            csv.AppendLine("ExpenseId,Description,TotalAmount,UserId,AmountOwed");

            foreach (var expense in expenses)
            {
                foreach (var participant in expense.ExpenseParticipants)
                {
                    csv.AppendLine($"{expense.ExpenseId},{expense.Description},{expense.TotalAmount},{participant.UserId},{participant.AmountOwed}");
                }
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }
    }
}
