using ECommerceApplication.Application.Features.ShoppingCarts.Queries;
using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.AddOrderItemToCart
{
    public class AddOrderItemToShoppingCartResponse :BaseResponse
    {
        public AddOrderItemToShoppingCartResponse() : base()
        {
        }

        public ShoppingCartDto ShoppingCart{ get; set; }
    }
}
