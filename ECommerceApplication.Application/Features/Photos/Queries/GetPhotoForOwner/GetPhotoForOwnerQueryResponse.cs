using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Photos.Queries.GetPhotoForOwner
{
    public class GetPhotoForOwnerQueryResponse : BaseResponse
    {
        public GetPhotoForOwnerQueryResponse() : base()
        {
        }
        public List<PhotoDto> Photos { get; set; }
    }
}