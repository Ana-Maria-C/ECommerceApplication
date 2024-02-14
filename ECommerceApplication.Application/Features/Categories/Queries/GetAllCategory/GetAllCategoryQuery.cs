using MediatR;

namespace ECommerceApplication.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQuery : IRequest<GetAllCategoryResponse>
    {
        public GetAllCategoryQuery() { }

    }
}
