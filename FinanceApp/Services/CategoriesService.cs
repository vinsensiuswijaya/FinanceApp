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

        public async Task AddAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(CategoryDTO updatedCategoryDto)
        {
            var category = _mapper.Map<Category>(updatedCategoryDto);
            _categoryRepository.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var category = await _categoryRepository.GetByIdAsync(Id);
            if (category != null)
            {
                _categoryRepository.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDTO>(category);
        }
    }
}