using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class Photo
    {
        private Photo(Guid ownerId, byte[] imageData)
        {
            PhotoId = Guid.NewGuid();
            OwnerId = ownerId;
            ImageData = imageData;

        }
        public Guid PhotoId { get; set; }
        public Guid OwnerId { get; set; }
        public byte[] ImageData { get; set; }

        public static Result<Photo> Create(Guid ownerId, byte[] imageData)
        {
            if (ownerId == Guid.Empty)
            {
                return Result<Photo>.Failure("Owner Id is required");
            }
            if (imageData == null)
            {
                return Result<Photo>.Failure("Image data is required");
            }
            return Result<Photo>.Success(new Photo(ownerId, imageData));
        }
    }
}