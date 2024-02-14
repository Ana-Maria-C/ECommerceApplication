using ECommerceApplication.Application.Responses;


namespace ECommerceApplication.Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQueryResponse : BaseResponse
    {
        public GetByIdProductQueryResponse() : base()
        {
        }
        public ProductDto Product { get; set; } = default!;
    }
}
