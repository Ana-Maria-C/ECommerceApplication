using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, UpdateAddressDto>
    {
        private readonly IAddressRepository addressRepository;

        public UpdateAddressCommandHandler(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<UpdateAddressDto> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateAddressCommandValidator(addressRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                return new UpdateAddressDto
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var @event = await addressRepository.FindByIdAsync(request.AddressId);
            if (@event == null)
            {
                return new UpdateAddressDto
                {
                    Success = false,
                    ValidationsErrors = ["Product not found"]
                };
            }
            @event.Value.Update(request.Street, request.City, request.State, request.PostalCode, request.IsDefault);
            await addressRepository.UpdateAsync(@event.Value);

            return new UpdateAddressDto
            {
                Success = true,
                AddressId = @event.Value.AddressId,
                Street = @event.Value.Street,
                City = @event.Value.City,
                State = @event.Value.State,
                PostalCode = @event.Value.PostalCode,
                IsDefault = @event.Value.IsDefault
            };
        }
    }
}