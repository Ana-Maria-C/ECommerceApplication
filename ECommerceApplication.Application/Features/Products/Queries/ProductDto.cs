namespace ECommerceApplication.Application.Features.Products.Queries
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public Guid CompanyId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public CategoryDto Category { get; set; }
    }
}
