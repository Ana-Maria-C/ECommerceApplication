using ECommerceApplication.Application.Features.ShoppingCarts.Queries;
using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.DeleteShoppingCart
{
    public class DeleteShoppingCartResponse : BaseResponse
    {
        public DeleteShoppingCartResponse() : base()
        {
        }

        public ShoppingCartDto ShoppingCart { get; set; }
    }
}
