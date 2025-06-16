using FinanceApp.Models;
using FinanceApp.Dtos;

namespace FinanceApp.Services
{
    public interface IExpensesService
    {
        // CREATE
        Task AddAsync(ExpenseDTO expenseDto);
        // READ
        Task<IEnumerable<ExpenseDTO>> GetAll();
        Task<ExpenseDTO> GetExpenseById(int id);
        // UPDATE
        Task EditAsync(ExpenseDTO expenseDto);
        // DELETE
        Task Delete(int id);

        Task<IEnumerable<ExpenseChartDataDTO>> GetChartData();
    }
}