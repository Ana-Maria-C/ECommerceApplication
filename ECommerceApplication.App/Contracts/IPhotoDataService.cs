using ECommerceApplication.App.Services;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;

namespace ECommerceApplication.App.Contracts
{
    public interface IPhotoDataService
    {
	    Task<ApiResponse<List<PhotoDto>>> GetPhotoForOwnerAsync(Guid ownerId);
        Task<ApiResponse<PhotoDto>> UploadPhotoAsync(Guid ownerId, Stream photoStream, string fileName);
		Task<ApiResponse<PhotoDto>> DeletePhotoAsync(Guid photoId);

    }
}
