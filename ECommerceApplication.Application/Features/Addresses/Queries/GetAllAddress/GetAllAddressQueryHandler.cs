using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Addresses.Queries.GetAllAddress
{
    public class GetAllAddressQueryHandler : IRequestHandler<GetAllAddressQuery, GetAllAddressResponse>
    {
        private readonly IAddressRepository repository;

        public GetAllAddressQueryHandler(IAddressRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllAddressResponse> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllAsync();
            var addresses = result.Value.Select(a => new AddressDto
            {
                AddressId = a.AddressId,
                UserId = a.UserId,
                Street = a.Street,
                City = a.City,
                State = a.State,
                PostalCode = a.PostalCode,
                IsDefault = a.IsDefault
            }).ToList();
            return new GetAllAddressResponse
            {
                Addresses = addresses,
                Success = true
            };
        }
    }
}
