using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;

namespace ECommerceApplication.App.Contracts
{
    public interface IUserDataService
    {
        Task<UserViewModel> GetUserByEmailAsync(string email);
        Task<ApiResponse<UpdateUserDto>> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
    }
}