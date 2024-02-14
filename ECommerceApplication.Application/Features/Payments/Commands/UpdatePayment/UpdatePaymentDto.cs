using ECommerceApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentDto : BaseResponse
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public string PaymentStatus { get; set; } 
        public string PaymentMethod { get; set; } 
        public DateTime PaymentDate { get; set; } 
        public decimal Amount { get; set; } 
        public string Currency { get; set; } 

    }
}
