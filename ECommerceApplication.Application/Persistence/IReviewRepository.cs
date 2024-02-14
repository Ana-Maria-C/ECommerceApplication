using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Persistence
{
    public interface IReviewRepository : IAsyncRepository<Review>
    {
        Task<Result<List<Review>>> GetReviewsByProductIdAsync(Guid productId);
    }
}
