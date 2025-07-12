using FinanceApp.Dtos;
using FinanceApp.Models;

namespace FinanceApp.Services
{
    public interface ICategoriesService
    {
        // CREATE
        Task AddAsync(CategoryDTO categoryDTO, string userId);
        // READ
        Task<IEnumerable<CategoryDTO>> GetAllByUserIdAsync(string userId);
        Task<CategoryDTO> GetByIdAsync(int id, string userId);
        // UPDATE
        Task EditAsync(CategoryDTO categoryDTO, string userId);
        // DELETE
        Task DeleteAsync(int Id, string userId);
    }
}