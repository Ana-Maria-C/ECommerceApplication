using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Persistence
{
    public interface IOrderItemRepository : IAsyncRepository<OrderItem>
    {
        Task<Result<IReadOnlyList<OrderItem>>> getOrderItemsByFilter(Guid? shoppingCartId, Guid? productId);
    }
}
