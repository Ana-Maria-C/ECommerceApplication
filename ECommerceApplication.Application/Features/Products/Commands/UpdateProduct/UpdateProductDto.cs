using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductDto : BaseResponse
    {
        public Guid ProductId { get; set; }
        public Guid CompanyId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}

