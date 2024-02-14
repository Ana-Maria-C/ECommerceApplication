using ECommerceApplication.Application.Persistence;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
    {
        private readonly IPaymentRepository _paymentRepository;
        public UpdatePaymentCommandValidator(IPaymentRepository paymentRepository)
        {
            /*RuleFor(updatePayment => updatePayment.PaymentId)
               .NotEmpty().WithMessage("PaymentId is required.")
               .MustAsync(async (paymentId, cancellation) =>
               {
                   return await PaymentExists(paymentId);
               }).WithMessage("PaymentId does not exist.");*/

            RuleFor(updatePayment => updatePayment.OrderId)
                .NotEmpty().WithMessage("OrderId is required.");

            RuleFor(updatePayment => updatePayment.PaymentStatus)
                .NotEmpty().WithMessage("PaymentStatus is required.");

            RuleFor(updatePayment => updatePayment.PaymentMethod)
                .NotEmpty().WithMessage("PaymentMethod is required.");

            RuleFor(updatePayment => updatePayment.PaymentDate)
                .NotEmpty().WithMessage("PaymentDate is required.")
                .Must(BeAValidDate).WithMessage("Invalid date format for PaymentDate.");

            RuleFor(updatePayment => updatePayment.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(updatePayment => updatePayment.Currency)
                .NotEmpty().WithMessage("Currency is required.");
        }

        private bool BeAValidDate(DateTime time)
        {
            return !time.Equals(default(DateTime));
        }

       /* private async Task<bool> PaymentExists(Guid paymentId)
        {
            var payment = await _paymentRepository.GetPaymentById(paymentId);
            return payment != null;
        }*/

    }   
}
