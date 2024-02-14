using ECommerceApplication.Application.Features.Addresses.Queries;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(ECommerceApplicationContext context) : base(context)
        {
        }

        public async Task<bool> AddressExists(Guid addressId)
        {
            return await context.Addresses.AnyAsync(a => a.AddressId == addressId);
        }

        public async Task<Result<List<Address>>> GetAddressByUserIdAsync(Guid userId)
        {
            var addresses = await context.Addresses
                .Where(x => x.UserId == userId)
                .ToListAsync();
            if (addresses.Count == 0)
            {
                return Result<List<Address>>.Failure($"Addresses for user with id {userId} not found");
            }
            return Result<List<Address>>.Success(addresses);
        }
    }
}
