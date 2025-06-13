using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data.Service
{
    public class ExpensesService : IExpensesService
    {
        private readonly FinanceAppContext _context;
        public ExpensesService(FinanceAppContext context)
        {
            _context = context;
        }
        public async Task Add(Expense expense)
        {
            if (expense.Date.Kind == DateTimeKind.Unspecified)
            {
                expense.Date = DateTime.SpecifyKind(expense.Date, DateTimeKind.Local).ToUniversalTime();
            }
            else
            {
                expense.Date =  expense.Date.ToUniversalTime();
            }
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }
        public async Task Edit(Expense updatedExpense)
        {
            var existingExpense = await _context.Expenses.FindAsync(updatedExpense.Id);

            existingExpense.Description = updatedExpense.Description;
            existingExpense.Amount = updatedExpense.Amount;
            existingExpense.Category = updatedExpense.Category;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Expense>> GetAll()
        {
            var expenses = await _context.Expenses
                                         .OrderBy(e => e.Date)
                                         .ToListAsync();
            return expenses;
        }

        public async Task<Expense> GetExpenseById(int id)
        {
            return await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Delete(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChangesAsync();
            }
        }
        public IQueryable GetChartData()
        {
            var data = _context.Expenses
                                .GroupBy(e => e.Category)
                                .Select(g => new
                                {
                                    Category = g.Key,
                                    Total = g.Sum(e => e.Amount)
                                });
            return data;
        }
    }
}