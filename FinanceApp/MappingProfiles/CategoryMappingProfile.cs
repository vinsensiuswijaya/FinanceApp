using AutoMapper;
using FinanceApp.Dtos;
using FinanceApp.Models;

namespace FinanceApp.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}