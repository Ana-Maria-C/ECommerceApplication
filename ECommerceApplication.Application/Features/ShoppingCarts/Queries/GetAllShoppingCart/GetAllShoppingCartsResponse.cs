using ECommerceApplication.Application.Responses;


namespace ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetAll
{
    public class GetAllShoppingCartsResponse : BaseResponse
    {
        public GetAllShoppingCartsResponse() : base()
        {
        }
        public List<ShoppingCartDto> ShoppingCarts { get; set; }
    }
}
