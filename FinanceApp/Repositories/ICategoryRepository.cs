using FinanceApp.Models;

namespace FinanceApp.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllByUserIdAsync(string userId);
    }
}