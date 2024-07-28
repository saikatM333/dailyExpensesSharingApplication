using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using repositoryLayer.Interface;
using modelLayer.model;

namespace DailyExpensesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense([FromBody] CreateExpenseDto expenseDto)
        {
            try
            {
                var createdExpense = await _expenseService.AddExpenseAsync(expenseDto);
                return Ok(createdExpense);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
           
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message); // Internal Server Error
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserExpenses(int userId)
        {
            try
            {
                var expenses = await _expenseService.GetUserExpensesAsync(userId);
                return Ok(expenses);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message); // Internal Server Error
            }
        }

        [HttpGet("overall")]
        public async Task<IActionResult> GetOverallExpenses()
        {
            try
            {
                var expenses = await _expenseService.GetOverallExpensesAsync();
                return Ok(expenses);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message); // Internal Server Error
            }
        }

        [HttpGet("balance-sheet")]
        public async Task<IActionResult> DownloadBalanceSheet()
        {
            var balanceSheet = await _expenseService.GetBalanceSheetAsync();
            return File(balanceSheet, "text/csv", "balance_sheet.csv");
        }
    
    }
}
