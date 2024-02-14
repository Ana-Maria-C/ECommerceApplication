using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandResponse : BaseResponse
    {
        public CreateCategoryCommandResponse() : base()
        {
        }

        public CreateCategoryDto Data { get; set; } = default!;
    }
}