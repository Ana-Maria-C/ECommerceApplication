using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;

namespace ECommerceApplication.App.Contracts
{
    public interface IReviewDataService
    {
        Task<List<ReviewViewModel>> GetReviewAsync();

        Task<ApiResponse<ReviewDto>> CreateReviewAsync(ReviewViewModel reviewViewModel);

        Task<List<ReviewViewModel>> GetReviewByProductId(Guid productId);
        Task<ApiResponse<ReviewDto>> DeleteReviewAsync(Guid reviewId);
    }
}
