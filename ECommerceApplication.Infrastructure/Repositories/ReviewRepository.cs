using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Infrastructure.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ECommerceApplicationContext context) : base(context)
        {
        }

        public async Task<Result<List<Review>>> GetReviewsByProductIdAsync(Guid productId)
        {
            var reviews = await context.Reviews
                .Where(x => x.ProductId == productId)
                .ToListAsync();
            if (reviews.Count == 0)
            {
                return Result<List<Review>>.Failure($"No reviews found for product id {productId}.");
            }
            return Result<List<Review>>.Success(reviews);
        }
    }
}
