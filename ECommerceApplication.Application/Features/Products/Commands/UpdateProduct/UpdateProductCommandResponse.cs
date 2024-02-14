using ECommerceApplication.Application.Features.Products.Queries;
using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandResponse : BaseResponse
    {
        public UpdateProductCommandResponse() : base()
        {
        }

        public ProductDto Product { get; set; } = default!;
    }
}