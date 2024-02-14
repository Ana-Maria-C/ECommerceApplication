using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Infrastructure.Repositories
{
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ECommerceApplicationContext context) : base(context)
        {

        }
        public async Task<Result<ShoppingCart>> FindByUserIdAsync(Guid userId)
        {
            var shoppingCart = await context.ShoppingCarts
                .Include(x => x.OrderItems)
                .ThenInclude(orderItem => orderItem.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId && !x.used);
            if (shoppingCart == null)
            {
                return Result<ShoppingCart>.Failure($"Shopping cart for user with id {userId} not found");
            }
            return Result<ShoppingCart>.Success(shoppingCart);
        }
    }
}
