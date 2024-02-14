
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;

namespace ECommerceApplication.App.Contracts
{
    public interface ICategoryDataService
    {
        Task<List<CategoryViewModel>> GetCategoriesAsync();

        Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CategoryViewModel categoryViewModel);

        Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(CategoryViewModel categoryViewModel);


    }
}
