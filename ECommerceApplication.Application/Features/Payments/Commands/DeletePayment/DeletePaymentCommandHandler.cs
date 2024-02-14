using ECommerceApplication.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Commands.DeletePayment
{
    public class DeletePaymentCommandHandler
    {
        private readonly IPaymentRepository _paymentRepository;

        public DeletePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<DeletePaymentCommandResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var result = await _paymentRepository.DeleteAsync(request.PaymentId);
            if (result.IsSuccess)
            {
                return new DeletePaymentCommandResponse
                {
                    Success = true
                };
            }
            return new DeletePaymentCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { result.Error }
            };
        }
    }
}
