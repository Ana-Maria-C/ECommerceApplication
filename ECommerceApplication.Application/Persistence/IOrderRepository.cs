using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<Result<IReadOnlyList<Order>>> getOrdersByFilter(Guid? UserId);
    }
}
