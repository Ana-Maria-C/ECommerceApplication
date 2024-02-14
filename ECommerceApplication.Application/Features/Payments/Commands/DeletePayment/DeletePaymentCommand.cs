using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Commands.DeletePayment
{
    public class DeletePaymentCommand : IRequest<DeletePaymentCommandResponse>
    {
        public Guid PaymentId { get; set; }
    }
}
