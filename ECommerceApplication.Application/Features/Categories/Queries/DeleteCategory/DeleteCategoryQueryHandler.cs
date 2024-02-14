using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Categories.Queries.DeleteCategory
{
    public class DeleteCategoryQueryHandler : IRequestHandler <DeleteCategoryQuery, DeleteCategoryResponse>
    {
       private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<DeleteCategoryResponse> Handle(DeleteCategoryQuery request, CancellationToken cancellationToken)
        {
            var response = new DeleteCategoryResponse();
            var category = await _categoryRepository.FindByIdAsync(request.CategoryId);
            if (category == null)
            {
                response.Success = false;
                response.ValidationsErrors = new List<string> { "Category not found" };
                return response;
            }
            await _categoryRepository.DeleteAsync(category.Value.CategoryId);
            response.Success = true;
            response.Message = "Category deleted successfully";
            return response;

        }
    }
}
