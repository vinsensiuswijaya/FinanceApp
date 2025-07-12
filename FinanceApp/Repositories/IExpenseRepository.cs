using FinanceApp.Models;
using FinanceApp.Dtos;

namespace FinanceApp.Repositories
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        public Task<IEnumerable<ExpenseChartDataDTO>> GetChartDataAsync();
        Task<IEnumerable<Expense>> GetAllByUserIdAsync(string userId);
        Task<IEnumerable<ExpenseChartDataDTO>> GetChartDataByUserIdAsync(string userId);
    }
}