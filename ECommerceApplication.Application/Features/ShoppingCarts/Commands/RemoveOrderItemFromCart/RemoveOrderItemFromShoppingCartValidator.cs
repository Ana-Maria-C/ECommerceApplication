using FluentValidation;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.RemoveOrderItemFromCart
{
    public class RemoveOrderItemFromShoppingCartValidator : AbstractValidator<RemoveOrderItemFromShoppingCartCommand>
    {
        public RemoveOrderItemFromShoppingCartValidator()
        {
            RuleFor(p => p.ShoppingCartId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
            RuleFor(p => p.OrderItemId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
