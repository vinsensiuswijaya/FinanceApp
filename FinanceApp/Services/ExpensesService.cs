using FinanceApp.Models;
using FinanceApp.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FinanceApp.Dtos;
using System.Threading.Tasks;

namespace FinanceApp.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly FinanceAppContext _context;
        private readonly IMapper _mapper;

        public ExpensesService(FinanceAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Add(ExpenseDTO expenseDto)
        {
            expenseDto.Date = DateTime.Now;
            var expense = _mapper.Map<Expense>(expenseDto);
            if (expense.Date.Kind == DateTimeKind.Unspecified)
            {
                expense.Date = DateTime.SpecifyKind(expense.Date, DateTimeKind.Local).ToUniversalTime();
            }
            else
            {
                expense.Date = expense.Date.ToUniversalTime();
            }
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(ExpenseDTO updatedExpenseDto)
        {
            var existingExpense = await _context.Expenses.FindAsync(updatedExpenseDto.Id);
            updatedExpenseDto.Date = existingExpense.Date;

            _mapper.Map(updatedExpenseDto, existingExpense);

            if (existingExpense.Date.Kind == DateTimeKind.Unspecified)
                existingExpense.Date = DateTime.SpecifyKind(existingExpense.Date, DateTimeKind.Local).ToUniversalTime();
            else
                existingExpense.Date = existingExpense.Date.ToUniversalTime();           

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAll()
        {
            var expenses = await _context.Expenses
                                         .OrderBy(e => e.Date)
                                         .ToListAsync();
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public async Task<ExpenseDTO> GetExpenseById(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            return expense == null ? null : _mapper.Map<ExpenseDTO>(expense);
        }

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