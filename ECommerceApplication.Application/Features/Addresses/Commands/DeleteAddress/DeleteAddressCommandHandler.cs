using ECommerceApplication.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommandHandler:IRequestHandler<DeleteAddressCommand, DeleteAddressCommandResponse>
    {
        private readonly IAddressRepository addressRepository;
        public DeleteAddressCommandHandler(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<DeleteAddressCommandResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var result = await addressRepository.DeleteAsync(request.AddressId);
            if(!result.IsSuccess)
            {
                return new DeleteAddressCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            else
            {
                return new DeleteAddressCommandResponse
                {
                    Success = true
                };
            }
        }
        }
    }
