using ECommerceApplication.Application.Features.Addresses.Queries;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Persistence
{
    public interface IAddressRepository : IAsyncRepository<Address>
    {
        Task<Result<List<Address>>>GetAddressByUserIdAsync(Guid userId);
    }
}
