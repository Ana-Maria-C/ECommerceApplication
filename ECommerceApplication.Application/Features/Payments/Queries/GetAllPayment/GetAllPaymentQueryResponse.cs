using ECommerceApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Queries.GetAllPayment
{
    public class GetAllPaymentQueryResponse :BaseResponse
    {
        public GetAllPaymentQueryResponse() : base()
        {
        }

        public List<PaymentDto> Payments { get; set; } = default!;
    }
}
