namespace ECommerceApplication.App.ViewModels
{
    public class ProductDto
    {
        public Guid ProductId { get;  set; }
        public string ProductName { get;  set; } = string.Empty;
        public decimal Price { get;  set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }

        public CategoryViewModel Category { get; set; } = new CategoryViewModel();
    }
}
