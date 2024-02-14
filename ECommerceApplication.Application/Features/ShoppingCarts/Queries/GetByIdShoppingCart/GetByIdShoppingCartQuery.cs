using ECommerceApplication.Application.Features.ShoppingCarts.Queries;
using MediatR;

namespace ECommerceApplication.Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdShoppingCartQuery : IRequest<ShoppingCartDto>
    {
        public Guid ShoppingCartId { get; set; } = default!;
    }
}
