namespace ECommerceApplication.Application.Features.Photos.Queries
{
    public class PhotoDto
    {
        public Guid PhotoId { get; set; }
        public Guid OwnerId { get; set; }
        public string ImageData { get; set; }
    }
}