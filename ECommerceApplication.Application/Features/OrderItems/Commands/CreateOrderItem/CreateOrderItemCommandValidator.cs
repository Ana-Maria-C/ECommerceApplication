using FluentValidation;

namespace ECommerceApplication.Application.Features.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateOrderItemCommandValidator()
        {
            //RuleFor(p => p.CategoryName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
