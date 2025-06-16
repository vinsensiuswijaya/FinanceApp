using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using FinanceApp.Data;
using FinanceApp.Dtos;
using AutoMapper;

namespace FinanceApp.Repositories
{
    public class ExpenseRepository : IGenericRepository<ExpenseDTO>, IExpenseRepository
    {
        private readonly FinanceAppContext _context;
        private readonly IMapper _mapper;

        public ExpenseRepository(FinanceAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task AddAsync(ExpenseDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<ExpenseDTO> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExpenseDTO>> FindAsync(Expression<Func<ExpenseDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllAsync()
        {
            var expenses = await _context.Expenses.OrderBy(e => e.Date).ToListAsync();
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses); //move the mapper to the service
        }

        public async Task<ExpenseDTO> GetByIdAsync(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            return expense == null ? null : _mapper.Map<ExpenseDTO>(expense); //move the mapper to the service
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

        public void Remove(ExpenseDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ExpenseDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}