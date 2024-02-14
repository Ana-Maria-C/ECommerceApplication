using ECommerceApplication.Application.Responses;


namespace ECommerceApplication.Application.Features.Addresses.Queries.GetAllAddress
{
    public class GetAllAddressResponse : BaseResponse
    {
        public GetAllAddressResponse() : base()
        {
        }
        public List<AddressDto> Addresses { get; set; } = default!;
    }
}
