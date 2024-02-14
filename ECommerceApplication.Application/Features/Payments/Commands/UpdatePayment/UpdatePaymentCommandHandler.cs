using ECommerceApplication.Application.Persistence;
using MediatR;


namespace ECommerceApplication.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, UpdatePaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;

        public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<UpdatePaymentDto> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePaymentCommandValidator(_paymentRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdatePaymentDto
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var @payment = await _paymentRepository.FindByIdAsync(request.PaymentId);
            if (@payment == null)
            {
                return new UpdatePaymentDto
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Payment with the provided ID does not exist." }
                };
            }

            @payment.Value.Update(request.OrderId, request.Amount, request.PaymentDate, request.PaymentMethod, request.PaymentStatus, request.Currency);

            await _paymentRepository.UpdateAsync(@payment.Value);

            return new UpdatePaymentDto
            {
                Success = true,
                PaymentId = @payment.Value.PaymentId,
                OrderId = @payment.Value.OrderId,
                Amount = @payment.Value.Amount,
                PaymentDate = @payment.Value.PaymentDate,
                PaymentMethod = @payment.Value.PaymentMethod,
                PaymentStatus = @payment.Value.PaymentStatus,
                Currency = @payment.Value.Currency,

            };
        }
    }
}
