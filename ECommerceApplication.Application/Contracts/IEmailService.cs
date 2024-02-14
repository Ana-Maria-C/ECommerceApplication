using ECommerceApplication.Application.Models;

namespace ECommerceApplication.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Mail email);
    }
}
