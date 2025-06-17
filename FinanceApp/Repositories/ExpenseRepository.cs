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

        public override async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _dbSet.Include(e => e.Category).ToListAsync();
        }

        public override async Task<Expense> GetByIdAsync(int id)
        {
            return await _dbSet.Include(e => e.Category)
                               .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<ExpenseChartDataDTO>> GetChartDataAsync()
        {
            var expenses = await _context.Expenses
                                         .Include(e => e.Category)
                                         .ToListAsync();
            var data = expenses
                        .GroupBy(e => e.Category.Name)
                        .Select(g => new ExpenseChartDataDTO
                        {
                            Category = g.Key,
                            Total = g.Sum(e => e.Amount)
                        })
                        .ToList();
            return data;
        }
    }
}