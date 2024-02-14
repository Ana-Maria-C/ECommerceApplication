using ECommerceApplication.Application.Features.Addresses.Queries;
using ECommerceApplication.Application.Features.Users.Queries;
using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Application.Persistence
{
    public interface IUserManager
    {
        Task<Result<UserDto>> FindByIdAsync(Guid userId);
        Task<Result<List<UserDto>>> GetAllAsync();
        Task<Result<UserDto>> DeleteAsync(Guid userId);
        Task<Result<UserDto>> UpdateAsync(UserDto user);
        Task<Result<UserDto>> UpdateRoleAsync(UserDto user, string role);
        Task<Result<UserDto>> FindByEmailAsync(string email);
    }
}
