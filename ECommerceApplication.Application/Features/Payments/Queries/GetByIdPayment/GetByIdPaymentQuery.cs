using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Queries.GetByIdPayment
{
    public class GetByIdPaymentQuery :IRequest<GetByIdPaymentQueryResponse>
    {
        public Guid PaymentId { get; set; }
    }
}
