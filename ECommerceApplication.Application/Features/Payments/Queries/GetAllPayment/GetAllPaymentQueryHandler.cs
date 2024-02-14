using ECommerceApplication.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Queries.GetAllPayment
{
    public class GetAllPaymentQueryHandler: IRequestHandler<GetAllPaymentQuery, GetAllPaymentQueryResponse>
    {
        private readonly IPaymentRepository paymentRepository;
        public GetAllPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<GetAllPaymentQueryResponse> Handle(GetAllPaymentQuery request, CancellationToken cancellationToken)
        {
           var result = await paymentRepository.GetAllAsync();
           var payments = result.Value.Select(e=> new PaymentDto
           {
               PaymentId = e.PaymentId,
               OrderId = e.OrderId,
               PaymentMethod = e.PaymentMethod,
               PaymentStatus = e.PaymentStatus,
               PaymentDate = e.PaymentDate,
               Amount = e.Amount,
               Currency = e.Currency
           }).ToList();
            return new GetAllPaymentQueryResponse
            {
                Payments = payments,
                Success = true
            };
        }
    }
}
