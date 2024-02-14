using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ECommerceApplicationContext context) : base(context)
        {
        }
        public async Task<Result<IReadOnlyList<Order>>> getOrdersByFilter(Guid? userId) {
            try
            {
                var orders = await context.Orders
                    .Where(order => (!userId.HasValue || order.UserId == userId.Value))
                    .Include(o => o.OrderItems).ThenInclude(orderItem => orderItem.Product)
                    .ToListAsync();

                return Result<IReadOnlyList<Order>>.Success(orders);
            }
            catch (Exception e)
            {
                return Result<IReadOnlyList<Order>>.Failure(e.Message);
            }
        }
    }
}
