using FinanceApp.Dtos;

namespace FinanceApp.Repositories
{
    public interface IExpenseRepository : IGenericRepository<ExpenseDTO>
    {
        public Task<IEnumerable<ExpenseChartDataDTO>> GetChartData();
    }
}