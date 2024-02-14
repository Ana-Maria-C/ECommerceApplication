using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.App.ViewModels
{
    public class PhotoViewModel
    {
	    public Guid PhotoId { get; set; }

	    public Guid OwnerId { get; set; }
        
        public byte[] ImageData { get; set; }
    }
}
