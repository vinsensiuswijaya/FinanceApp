using FinanceApp.Models;
using FinanceApp.Dtos;

namespace FinanceApp.Repositories
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        public Task<IEnumerable<ExpenseChartDataDTO>> GetChartDataAsync();
    }
}