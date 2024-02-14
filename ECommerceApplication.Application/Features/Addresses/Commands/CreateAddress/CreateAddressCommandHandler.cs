using ECommerceApplication.Application.Contracts;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApplication.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IEmailService emailService;
        private readonly ILogger<CreateAddressCommandHandler> logger;

        public CreateAddressCommandHandler(IAddressRepository repository, IEmailService emailService, ILogger<CreateAddressCommandHandler> logger)
        {
            this.addressRepository = repository;
            this.emailService = emailService;
            this.logger = logger;
        }

        public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAddressCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateAddressCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var @address = Address.Create(request.UserId, request.Street, request.City, request.State, request.PostalCode, request.IsDefault);
            if (@address.IsSuccess)
            {

                await addressRepository.AddAsync(@address.Value);

                return new CreateAddressCommandResponse
                {
                    Success = true,
                    Address = new CreateAddressDto
                    {
                        AddressId = @address.Value.AddressId,
                        UserId = @address.Value.UserId,
                        Street = @address.Value.Street,
                        City = @address.Value.City,
                        State = @address.Value.State,
                        PostalCode = @address.Value.PostalCode,
                        IsDefault = address.Value.IsDefault
                    }
                };
            }

            return new CreateAddressCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { address.Error }
            };
        }
    }
}
