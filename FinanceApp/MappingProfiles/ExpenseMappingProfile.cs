using AutoMapper;
using FinanceApp.Dtos;
using FinanceApp.Models;

namespace FinanceApp.MappingProfiles
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<Expense, ExpenseDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ExpenseDTO, Expense>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());
        }
    }
}