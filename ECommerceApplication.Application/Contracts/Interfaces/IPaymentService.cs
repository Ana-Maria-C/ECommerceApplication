using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Contracts.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentAsync(Payment payment, string cardToken);

    }
}
