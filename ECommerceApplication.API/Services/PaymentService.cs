using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApplication.Application.Contracts.Interfaces;
using ECommerceApplication.Domain.Entities;
using Stripe;

namespace ECommerceApplication.API.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<string> CreatePaymentAsync(Payment payment, string cardToken)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(payment.Amount * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                    Description = $"Order ID: {payment.OrderId}",
                    Metadata = new Dictionary<string, string>
                    {
                        {"OrderId", payment.OrderId.ToString()},
                        {"PaymentDate", payment.PaymentDate.ToString()},
                        {"PaymentMethod", payment.PaymentMethod},
                        {"PaymentStatus", payment.PaymentStatus.ToString()},
                        {"PaymentId", payment.PaymentId.ToString()},
                    },
                    PaymentMethod = cardToken,
                    Confirm = true
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);
                return paymentIntent.Id;
            }
            catch (StripeException stripeEx)
            {
                Console.WriteLine("Stripe Exception:");
                Console.WriteLine(stripeEx.Message);
                Console.WriteLine(stripeEx.StripeError);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Exception:");
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
