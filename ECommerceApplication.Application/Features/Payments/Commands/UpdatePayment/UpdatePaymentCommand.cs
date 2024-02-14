using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommand : IRequest<UpdatePaymentDto>
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set;}
        public decimal Amount { get; set; } = 0;
        public string PaymentStatus { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string Currency { get; set; } = string.Empty;
    }
}
