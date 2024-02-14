using ECommerceApplication.Application.Responses;


namespace ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetByUserIdShoppingCart
{
    public class GetByUserIdShoppingCartResponse : BaseResponse
    {
        public GetByUserIdShoppingCartResponse() : base()
        {
        }
        public ShoppingCartDto Data { get; set; }
    }
}
