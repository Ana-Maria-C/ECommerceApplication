using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Addresses.Queries.GetAddressByUserId
{
    public class GetAddressByUserIdQueryHandler : IRequestHandler<GetAddressByUserIdQuery, GetAddressByUserIdResponse>
    {
        private readonly IAddressRepository _addressRepository;

        public GetAddressByUserIdQueryHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<GetAddressByUserIdResponse> Handle(GetAddressByUserIdQuery request, CancellationToken cancellationToken)
        {
            var addressResult = await _addressRepository.GetAddressByUserIdAsync(request.UserId);

            if (!addressResult.IsSuccess)
            {
                return new GetAddressByUserIdResponse
                {
                    Success = false,
                    Addresses = null
                };
            }

            var addresses = addressResult.Value;

            var addressDtos = new List<AddressDto>();

            foreach (var address in addresses)
            {
                addressDtos.Add(new AddressDto
                {
                    AddressId = address.AddressId,
                    UserId = address.UserId,
                    Street = address.Street,
                    City = address.City,
                    State = address.State,
                    PostalCode = address.PostalCode,
                    IsDefault = address.IsDefault
                });
            }

            return new GetAddressByUserIdResponse
            {
                Success = true,
                Addresses = addressDtos
            };
        }
    }
}