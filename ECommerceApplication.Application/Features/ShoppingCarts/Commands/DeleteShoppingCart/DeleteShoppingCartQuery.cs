using MediatR;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.DeleteShoppingCart
{
    public class DeleteShoppingCartQuery : IRequest<DeleteShoppingCartResponse>
    {
        public Guid ShoppingCartId { get; set; } = default!;

    }
}
