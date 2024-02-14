using FluentValidation;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.DeleteShoppingCart
{
    public class DeleteShoppingCartValidator : AbstractValidator<DeleteShoppingCartQuery>
    {
        public DeleteShoppingCartValidator()
        {
            RuleFor(p => p.ShoppingCartId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
