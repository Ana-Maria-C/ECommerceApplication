using MediatR;

namespace ECommerceApplication.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand :IRequest<CreatePaymentCommandResponse>
    {
        public Guid PaymentId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public string PaymentMethod { get; private set; }
        public string Currency { get; private set; }
        public string PaymentStatus { get; private set; }
    }
}
