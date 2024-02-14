using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryDto
    {
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
