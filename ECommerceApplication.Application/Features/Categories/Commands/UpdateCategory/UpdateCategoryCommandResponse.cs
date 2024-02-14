using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandResponse : BaseResponse
    {
        public UpdateCategoryCommandResponse() : base()
        {
        }

        public UpdateCategoryDto Category { get; set; }
    }
}