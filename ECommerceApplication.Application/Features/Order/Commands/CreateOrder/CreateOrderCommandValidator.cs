using FluentValidation;

namespace ECommerceApplication.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            //RuleFor(p => p.CategoryName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
