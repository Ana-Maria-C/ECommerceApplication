using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Infrastructure.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ECommerceApplicationContext context) : base(context)
        {
        }

        public async Task<Result<IReadOnlyList<OrderItem>>> getOrderItemsByFilter(Guid? shoppingCartId, Guid? productId)
        {
            try
            {
                var orderItems = await context.OrderItem
                    .Where(orderItem => (shoppingCartId == null || orderItem.ShoppingCartId == shoppingCartId) &&
                    (productId == null || orderItem.ProductId == productId))
                    .ToListAsync();

                return Result<IReadOnlyList<OrderItem>>.Success(orderItems);
            }
            catch (Exception e)
            {
                return Result<IReadOnlyList<OrderItem>>.Failure(e.Message);
            }

        }
    }
}
