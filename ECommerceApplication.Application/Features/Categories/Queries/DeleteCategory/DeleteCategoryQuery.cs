using MediatR;

namespace ECommerceApplication.Application.Features.Categories.Queries.DeleteCategory
{
    public class DeleteCategoryQuery : IRequest<DeleteCategoryResponse>
    {
        public Guid CategoryId { get; set; } = default!;

    }
}
