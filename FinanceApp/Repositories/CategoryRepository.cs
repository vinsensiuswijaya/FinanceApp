using FinanceApp.Data;
using FinanceApp.Models;

namespace FinanceApp.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FinanceAppContext context) : base(context) { }
    }
}