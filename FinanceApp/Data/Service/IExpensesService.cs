using FinanceApp.Models;

namespace FinanceApp.Data.Service
{
    public interface IExpensesService
    {
        // CREATE
        Task Add(Expense expense);
        // READ
        Task<IEnumerable<Expense>> GetAll();
        Task<Expense> GetExpenseById(int id);
        // UPDATE
        Task Edit(Expense expense);
        // DELETE
        Task Delete(int id);

        IQueryable GetChartData();
    }
}