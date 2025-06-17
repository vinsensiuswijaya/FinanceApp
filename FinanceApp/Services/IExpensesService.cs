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
        Task<ExpenseDTO> GetByIdAsync(int id);
        // UPDATE
        Task EditAsync(ExpenseDTO expenseDto);
        // DELETE
        Task DeleteAsync(int id);

        Task<IEnumerable<ExpenseChartDataDTO>> GetChartDataAsync();
    }
}