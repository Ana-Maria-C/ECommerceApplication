using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandResponse :BaseResponse
    {
        public CreatePaymentCommandResponse() : base()
        {
        }

        public CreatePaymentDto Payment { get; set; } = default!;
    }
}
