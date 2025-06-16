using FinanceApp.Dtos;
using FinanceApp.Models;

namespace FinanceApp.Services
{
    public interface ICategoriesService
    {
        // CREATE
        Task AddAsync(CategoryDTO categoryDTO);
        // READ
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(int id);
        // UPDATE
        Task EditAsync(CategoryDTO categoryDTO);
        // DELETE
        Task DeleteAsync(int Id);
    }
}