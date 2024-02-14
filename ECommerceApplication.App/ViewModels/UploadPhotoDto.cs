using Microsoft.AspNetCore.Http;

namespace ECommerceApplication.App.ViewModels
{
	public class UploadPhotoDto
	{
		public Guid OwnerId { get; set; }

		public IFormFile Photo { get; set; }
	}
}