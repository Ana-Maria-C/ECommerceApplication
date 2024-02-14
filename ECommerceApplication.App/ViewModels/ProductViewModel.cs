using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ECommerceApplication.App.ViewModels
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public Guid CompanyId { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Product price is required")]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public string StringCategoryId { get; set; } = string.Empty;

        public CategoryViewModel Category { get; set; } = new CategoryViewModel();

        public bool IsAddingToCart { get; set; }

        public IFormFile Photo { get; set; }

        public static ProductViewModel FromDto(ProductDto dto)
        {
            return new ProductViewModel
            {
                ProductId = dto.ProductId,
                CompanyId = dto.CategoryId,
                ProductName = dto.ProductName,
                Price = dto.Price,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                StringCategoryId = dto.CategoryId.ToString(),
                Category = new CategoryViewModel
                {
                    CategoryId = dto.Category.CategoryId,
                    CategoryName = dto.Category.CategoryName
                },
                IsAddingToCart = false
            };
        }

        public override bool Equals(object? obj)
        {
            return obj is ProductViewModel model &&
                   ProductId.Equals(model.ProductId) &&
                   CategoryId.Equals(model.CategoryId);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ProductId, CategoryId);
        }
    }
}
