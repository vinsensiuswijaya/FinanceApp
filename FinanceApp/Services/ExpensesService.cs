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
        private IExpenseRepository _expenseRepository;

        public ExpensesService(FinanceAppContext context, IMapper mapper)
        {
            _mapper = mapper;
            _expenseRepository = new ExpenseRepository(context);
        }

        public async Task AddAsync(ExpenseDTO expenseDto)
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

            await _expenseRepository.AddAsync(expense);
        }

        public async Task EditAsync(ExpenseDTO updatedExpenseDto)
        {
            var existingExpense = await _expenseRepository.GetByIdAsync(updatedExpenseDto.Id);
            updatedExpenseDto.Date = existingExpense.Date;

            _mapper.Map(updatedExpenseDto, existingExpense);

            if (existingExpense.Date.Kind == DateTimeKind.Unspecified)
                existingExpense.Date = DateTime.SpecifyKind(existingExpense.Date, DateTimeKind.Local).ToUniversalTime();
            else
                existingExpense.Date = existingExpense.Date.ToUniversalTime();

            await _expenseRepository.UpdateAsync(existingExpense);
        }

        public async Task Delete(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            if (expense != null)
            {
                await _expenseRepository.RemoveAsync(expense);
            }
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAll()
        {
            var expenses = await _expenseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public async Task<ExpenseDTO> GetExpenseById(int id)
        {
            var expense = await _expenseRepository.GetByIdAsync(id);
            return expense == null ? null : _mapper.Map<ExpenseDTO>(expense);
        }

        public async Task<IEnumerable<ExpenseChartDataDTO>> GetChartData()
        {
            var data = await _expenseRepository.GetChartData();
            return data;
        }
    }
}