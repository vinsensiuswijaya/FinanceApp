using FluentValidation;
using FinanceApp.Dtos;

namespace FinanceApp.Validators
{
    public class ExpenseValidator : AbstractValidator<ExpenseDTO>
    {
        public ExpenseValidator()
        {
            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Description is required!")
                .Length(1, 100).WithMessage("Description must be between 1 and 100 characters")
                .Matches(@"^[a-zA-Z0-9\s]+$").WithMessage("Description can only contain letters, numbers, and spaces");

            RuleFor(e => e.Amount)
                .NotEmpty().WithMessage("Amount is required!")
                .GreaterThan(0).WithMessage("Amount has to be more than 0!");

            RuleFor(e => e.CategoryId)
                .NotEmpty().WithMessage("Category is required")
                .GreaterThan(0).WithMessage("Category must be selected!");
        }
    }
}