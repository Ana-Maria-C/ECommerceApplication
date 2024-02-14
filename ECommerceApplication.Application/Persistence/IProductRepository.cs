using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Persistence
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<bool> ProductExists(Guid productId);

        Task<Result<List<Product>>> GetProductByCategoryIdAsync(Guid userId);
    }
}
