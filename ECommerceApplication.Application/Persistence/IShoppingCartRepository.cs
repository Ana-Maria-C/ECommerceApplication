using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Persistence
{
    public interface IShoppingCartRepository : IAsyncRepository<ShoppingCart>
    {
        Task<Result<ShoppingCart>> FindByUserIdAsync(Guid userId); 
    }
}
