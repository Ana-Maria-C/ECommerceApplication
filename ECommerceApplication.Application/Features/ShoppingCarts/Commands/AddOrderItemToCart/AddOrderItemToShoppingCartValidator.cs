using FluentValidation;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.AddOrderItemToCart
{
    public class AddOrderItemToShoppingCartValidator : AbstractValidator<AddOrderItemToShoppingCartCommand>
    {
        public AddOrderItemToShoppingCartValidator()
        {
            RuleFor(p => p.ShoppingCartId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
            RuleFor(p => p.OrderItemId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
