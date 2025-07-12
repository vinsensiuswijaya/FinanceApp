using FinanceApp.Models;
using FinanceApp.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FinanceApp.Dtos;
using FinanceApp.Repositories;

namespace FinanceApp.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly IMapper _mapper;
        private readonly FinanceAppContext _context;
        private IExpenseRepository _expenseRepository;

        public ExpensesService(FinanceAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _expenseRepository = new ExpenseRepository(context);
        }

        public async Task AddAsync(ExpenseDTO expenseDto, string userId)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            expense.UserId = userId;
            if (expense.Date.Kind == DateTimeKind.Unspecified)
            {
                expense.Date = DateTime.SpecifyKind(expense.Date, DateTimeKind.Local).ToUniversalTime();
            }
            else
            {
                expense.Date = expense.Date.ToUniversalTime();
            }

            await _expenseRepository.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ExpenseDTO updatedExpenseDto, string userId)
        {
            var existingExpense = await _expenseRepository.GetByIdAsync(updatedExpenseDto.Id);

            if (existingExpense == null || existingExpense.UserId != userId)
                throw new UnauthorizedAccessException("Expense not found or access denied.");

            _mapper.Map(updatedExpenseDto, existingExpense);
            existingExpense.UserId = userId; // Ensure user ID is maintained

            if (existingExpense.Date.Kind == DateTimeKind.Unspecified)
                existingExpense.Date = DateTime.SpecifyKind(existingExpense.Date, DateTimeKind.Local).ToUniversalTime();
            else
                existingExpense.Date = existingExpense.Date.ToUniversalTime();

            _expenseRepository.Update(existingExpense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense != null && expense.UserId == userId)
            {
                _expenseRepository.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllByUserIdAsync(string userId)
        {
            var expenses = await _expenseRepository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public async Task<ExpenseDTO> GetByIdAsync(int id, string userId)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense == null || expense.UserId != userId)
                return null;

            return _mapper.Map<ExpenseDTO>(expense);
        }

        public async Task<IEnumerable<ExpenseChartDataDTO>> GetChartDataByUserIdAsync(string userId)
        {
            var data = await _expenseRepository.GetChartDataByUserIdAsync(userId);
            return data;
        }
    }
}