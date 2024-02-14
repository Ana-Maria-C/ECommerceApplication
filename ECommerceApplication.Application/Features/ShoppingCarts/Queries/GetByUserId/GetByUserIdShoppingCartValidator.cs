using ECommerceApplication.Application.Features.Categories.Queries.GetByIdCategory;
using ECommerceApplication.Application.Features.Categories.Queries.GetByUserIdShoppingCart;
using FluentValidation;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetByUserIdShoppingCart
{
    internal class GetByUserIdShoppingCartValidator : AbstractValidator<GetByUserIdShoppingCartQuery>
    {
        public GetByUserIdShoppingCartValidator()
        {
            RuleFor(p => p.UserId)
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
