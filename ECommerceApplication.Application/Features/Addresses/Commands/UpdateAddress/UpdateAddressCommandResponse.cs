using ECommerceApplication.Application.Features.Addresses.Queries;
using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandResponse : BaseResponse
    {
        public UpdateAddressCommandResponse() : base()
        {
        }

        public AddressDto Address { get; set; } = default!;
    }
}