using ECommerceApplication.Application.Features.Products.Queries;
using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandResponse : BaseResponse
    {
        public CreateProductCommandResponse() : base()
        {
        }

        public ProductDto Product { get; set; } = default!;
    }
}