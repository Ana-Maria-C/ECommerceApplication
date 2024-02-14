using ECommerceApplication.Application.Responses;


namespace ECommerceApplication.Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductQueryResponse : BaseResponse
    {
        public GetAllProductQueryResponse() : base()
        {
        }
        public List<ProductDto> Products { get; set; } = default!;
    }
}
