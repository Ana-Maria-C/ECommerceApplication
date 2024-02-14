using MediatR;

namespace ECommerceApplication.Application.Features.Photos.Queries.GetPhotoForOwner
{
    public class GetPhotoForOwnerQuery : IRequest<GetPhotoForOwnerQueryResponse>
    {
        public Guid OwnerId { get; set; }
    }
}