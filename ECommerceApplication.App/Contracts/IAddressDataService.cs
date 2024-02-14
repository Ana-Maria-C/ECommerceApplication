using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;

namespace ECommerceApplication.App.Contracts
{
    public interface IAddressDataService
    {
        Task<List<AddressViewModel>> GetAddressesAsync();

        Task<ApiResponse<AddressDto>> CreateAddressAsync(AddressViewModel addressViewModel);

        Task<ApiResponse<AddressDto>> UpdateAddressAsync(AddressViewModel addressViewModel);

        Task<List<AddressViewModel>> GetUserAddressesAsync(Guid userId);

        Task<ApiResponse<AddressDto>> DeleteAddressAsync(Guid addressId);
    }
}
