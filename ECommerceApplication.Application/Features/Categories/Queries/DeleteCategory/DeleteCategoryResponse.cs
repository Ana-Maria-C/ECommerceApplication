using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Categories.Queries.DeleteCategory
{
    public class DeleteCategoryResponse : BaseResponse
    {
        public DeleteCategoryResponse() : base()
        {
        }

        public CategoryDto Category { get; set; }
    }
}
