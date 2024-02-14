using FluentValidation;

namespace ECommerceApplication.Application.Features.OrderItems.Commands.DeleteCategory
{
    public class DeleteOrderItemValidator : AbstractValidator<DeleteOrderItemQuery>
    {
        public DeleteOrderItemValidator()
        {
            RuleFor(p => p.OrderItemId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
