using AutoMapper;
using FinanceApp.Data;
using FinanceApp.Dtos;
using FinanceApp.Models;
using FinanceApp.Repositories;

namespace FinanceApp.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IMapper _mapper;
        private readonly FinanceAppContext _context;
        private ICategoryRepository _categoryRepository;

        public CategoriesService(FinanceAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _categoryRepository = new CategoryRepository(context);
        }

        public async Task AddAsync(CategoryDTO categoryDto, string userId)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.UserId = userId;
            await _categoryRepository.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(CategoryDTO updatedCategoryDto, string userId)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(updatedCategoryDto.Id);
            
            if (existingCategory == null || existingCategory.UserId != userId)
                throw new UnauthorizedAccessException("Category not found or access denied.");

            _mapper.Map(updatedCategoryDto, existingCategory);
            existingCategory.UserId = userId; // Ensure user ID is maintained
            _categoryRepository.Update(existingCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id, string userId)
        {
            var category = await _categoryRepository.GetByIdAsync(Id);
            if (category != null && category.UserId == userId)
            {
                _categoryRepository.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllByUserIdAsync(string userId)
        {
            var categories = await _categoryRepository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetByIdAsync(int id, string userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.UserId != userId)
                return null;

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}