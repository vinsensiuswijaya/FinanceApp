using FluentValidation;
using FinanceApp.Dtos;

namespace FinanceApp.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required!")
                .Length(1, 50).WithMessage("Name must be between 1 and 50 characters")
                .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Name can only contain letters, numbers, and spaces");
        }
    }
}