using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommand:IRequest<DeleteAddressCommandResponse>
    {
        public Guid AddressId { get; set; }
    }
}
