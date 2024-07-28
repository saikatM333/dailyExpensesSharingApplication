using modelLayer.model;
using repositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repositoryLayer.Interface
{
    public interface IExpenseService
    {
        Task<Expense> AddExpenseAsync(CreateExpenseDto expenseDto);
        Task<IEnumerable<Expense>> GetUserExpensesAsync(int userId);
        Task<IEnumerable<Expense>> GetOverallExpensesAsync();
        Task<byte[]> GetBalanceSheetAsync();
    }
}
