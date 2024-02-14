using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.CreateShoppingCart
{
    public class CreateShoppingCartCommandResponse : BaseResponse
    {
        public CreateShoppingCartCommandResponse() : base()
        {
        }

        public CreateShoppingCartDto ShoppingCart { get; set; }
    }
}