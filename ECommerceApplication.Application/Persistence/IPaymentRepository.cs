using ECommerceApplication.Application.Features.Payments.Commands.UpdatePayment;
using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Persistence
{
    public interface IPaymentRepository : IAsyncRepository<Payment>
    {
    }
}
