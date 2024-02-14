using ECommerceApplication.Application.Features.Categories.Queries.GetByIdCategory;
using FluentValidation;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetByIdShoppingCart
{
    internal class GetByIdShoppingCartValidator : AbstractValidator<GetByIdShoppingCartQuery>
    {
        public GetByIdShoppingCartValidator()
        {
            RuleFor(p => p.ShoppingCartId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
