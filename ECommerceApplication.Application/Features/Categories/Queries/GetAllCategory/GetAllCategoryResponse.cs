using ECommerceApplication.Application.Responses;


namespace ECommerceApplication.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryResponse : BaseResponse
    {
        public GetAllCategoryResponse() : base()
        {
        }
        public List<CategoryDto> Categories { get; set; }
    }
}
