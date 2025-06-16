using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using FinanceApp.Data;
using FinanceApp.Models;
using FinanceApp.Dtos;

namespace FinanceApp.Repositories
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(FinanceAppContext context) : base(context) { }

        public async Task<IEnumerable<ExpenseChartDataDTO>> GetChartData()
        {
            var data = await _context.Expenses
                                     .GroupBy(e => e.Category)
                                     .Select(g => new ExpenseChartDataDTO
                                     {
                                         Category = g.Key,
                                         Total = g.Sum(e => e.Amount)
                                     })
                                     .ToListAsync();
            return data;
        }
    }
}