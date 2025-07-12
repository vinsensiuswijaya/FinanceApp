using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FinanceAppContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetAllByUserIdAsync(string userId)
        {
            return await _dbSet.Where(c => c.UserId == userId).ToListAsync();
        }
    }
}