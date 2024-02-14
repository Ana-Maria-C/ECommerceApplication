using ECommerceApplication.Application.Features.Products.Queries;
using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Products.Queries.GetProductByCategoryId
{
    public class GetProductByCategoryIdResponse : BaseResponse
    {
        public List<ProductDto> Products { get; set; }
    }
}