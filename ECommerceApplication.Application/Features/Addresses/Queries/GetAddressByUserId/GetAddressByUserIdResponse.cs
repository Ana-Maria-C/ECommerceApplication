using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Addresses.Queries.GetAddressByUserId
{
    public class GetAddressByUserIdResponse : BaseResponse
    {
        public List<AddressDto> Addresses { get; set; }
    }
}