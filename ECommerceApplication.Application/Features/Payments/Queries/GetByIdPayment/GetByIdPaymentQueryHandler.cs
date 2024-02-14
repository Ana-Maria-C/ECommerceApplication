using ECommerceApplication.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Queries.GetByIdPayment
{
    public class GetByIdPaymentQueryHandler : IRequestHandler <GetByIdPaymentQuery, GetByIdPaymentQueryResponse>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetByIdPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<GetByIdPaymentQueryResponse> Handle(GetByIdPaymentQuery request, CancellationToken cancellationToken)
        { 
            var @payment =await _paymentRepository.FindByIdAsync(request.PaymentId);
            if (!@payment.IsSuccess)
            {
                return new GetByIdPaymentQueryResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { @payment.Error }
                };
            }

            return new GetByIdPaymentQueryResponse
            {
                Success = true,
                Payment = new PaymentDto
                {
                    PaymentId = @payment.Value.PaymentId,
                    OrderId = @payment.Value.OrderId,
                    Amount = @payment.Value.Amount,
                    PaymentDate = @payment.Value.PaymentDate,
                    PaymentMethod = @payment.Value.PaymentMethod,
                    PaymentStatus = @payment.Value.PaymentStatus,
                    Currency = @payment.Value.Currency
                }
            };
        }   
    }
}
