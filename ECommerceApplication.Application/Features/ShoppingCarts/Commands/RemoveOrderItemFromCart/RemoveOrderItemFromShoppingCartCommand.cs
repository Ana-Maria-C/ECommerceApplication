using MediatR;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.RemoveOrderItemFromCart
{
    public class RemoveOrderItemFromShoppingCartCommand : IRequest<RemoveOrderItemFromShoppingCartResponse> 
    {
        public Guid ShoppingCartId { get; set; }
        public Guid OrderItemId { get; set; }
    }
}
