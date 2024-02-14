using MediatR;

namespace ECommerceApplication.Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQuery : IRequest<CategoryDto>
    {
        public Guid CategoryId { get; set; } = default!;
    }
}
