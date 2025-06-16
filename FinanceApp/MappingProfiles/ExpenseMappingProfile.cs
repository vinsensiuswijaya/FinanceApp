using AutoMapper;
using FinanceApp.Dtos;
using FinanceApp.Models;

namespace FinanceApp.MappingProfiles
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<Expense, ExpenseDTO>();
            CreateMap<ExpenseDTO, Expense>();
        }
    }
}