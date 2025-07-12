using FinanceApp.Models;
using FinanceApp.Dtos;

namespace FinanceApp.Services
{
    public interface IExpensesService
    {
        // CREATE
        Task AddAsync(ExpenseDTO expenseDto, string userId);
        // READ
        Task<IEnumerable<ExpenseDTO>> GetAllByUserIdAsync(string userId);
        Task<ExpenseDTO> GetByIdAsync(int id, string userId);
        // UPDATE
        Task EditAsync(ExpenseDTO expenseDto, string userId);
        // DELETE
        Task DeleteAsync(int id, string userId);

        Task<IEnumerable<ExpenseChartDataDTO>> GetChartDataByUserIdAsync(string userId);
    }
}