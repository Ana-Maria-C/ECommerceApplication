using ECommerceApplication.App.Services;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;

namespace ECommerceApplication.App.Contracts
{
    public interface IProductDataService
    {
        Task<List<ProductViewModel>> GetProductAsync();
        Task<ProductViewModel> GetProductByIdAsync(Guid productId);
        Task <ApiResponseForProduct> CreateProductAsync(ProductViewModel productViewModel);
        Task<ApiResponse<ProductDto>> UpdateProductAsync(ProductViewModel productViewModel);
    }
}
