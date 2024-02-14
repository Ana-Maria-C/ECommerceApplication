using ECommerceApplication.Application.Features.ShoppingCarts.Queries;
using ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetByUserIdShoppingCart;
using MediatR;

namespace ECommerceApplication.Application.Features.Categories.Queries.GetByUserIdShoppingCart
{
    public class GetByUserIdShoppingCartQuery : IRequest<GetByUserIdShoppingCartResponse>
    {
        public Guid UserId { get; set; } = default!;
    }
}
