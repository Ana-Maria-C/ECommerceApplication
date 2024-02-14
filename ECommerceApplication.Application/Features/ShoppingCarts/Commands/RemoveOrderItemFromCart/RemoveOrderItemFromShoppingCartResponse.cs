using ECommerceApplication.Application.Features.ShoppingCarts.Queries;
using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.RemoveOrderItemFromCart
{
    public class RemoveOrderItemFromShoppingCartResponse :BaseResponse
    {
        public RemoveOrderItemFromShoppingCartResponse() : base()
        {
        }

        public ShoppingCartDto ShoppingCart{ get; set; }
    }
}
