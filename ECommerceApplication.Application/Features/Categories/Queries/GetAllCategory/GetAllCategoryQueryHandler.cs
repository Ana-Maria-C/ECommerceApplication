using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, GetAllCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetAllCategoryResponse> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            GetAllCategoryResponse response = new();
            var result = await _categoryRepository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Categories = result.Value.Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                }).ToList();
            }
            return response;
        }
    }
}
