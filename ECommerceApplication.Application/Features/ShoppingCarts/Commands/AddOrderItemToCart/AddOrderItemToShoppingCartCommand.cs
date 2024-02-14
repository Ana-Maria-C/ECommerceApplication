using MediatR;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.AddOrderItemToCart
{
    public class AddOrderItemToShoppingCartCommand : IRequest<AddOrderItemToShoppingCartResponse> 
    {
        public Guid ShoppingCartId { get; set; }
        public Guid OrderItemId { get; set; }
    }
}
