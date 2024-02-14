namespace ECommerceApplication.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductDto
    {
        public Guid ProductId { get; set; }
        public Guid CompanyId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; private set; }
        public string? ImageUrl { get; private set; }
        public Guid CategoryId { get; private set; }
        //public Guid ManufacturerId { get; private set; }
    }
}

