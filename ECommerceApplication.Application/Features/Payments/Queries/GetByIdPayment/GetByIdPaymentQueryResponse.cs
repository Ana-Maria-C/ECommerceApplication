using ECommerceApplication.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Queries.GetByIdPayment
{
    public class GetByIdPaymentQueryResponse : BaseResponse
    {
        public GetByIdPaymentQueryResponse() : base()
        {
        }

        public PaymentDto Payment { get; set; } = default!;
    }
}
