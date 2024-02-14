using ECommerceApplication.Application.Contracts;
using ECommerceApplication.Application.Features.Payments.Queries;
using ECommerceApplication.Application.Models;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Stripe.Climate;

namespace ECommerceApplication.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentCommandResponse>
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly ILogger<CreatePaymentCommandHandler> logger;
        private readonly IEmailService emailService;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, ILogger<CreatePaymentCommandHandler> logger, IEmailService emailService)
        {
            this.paymentRepository = paymentRepository;
            this.logger = logger;
            this.emailService = emailService;
        }

        public async Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePaymentCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new CreatePaymentCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var @payment = Payment.Create(request.OrderId, request.Amount, request.PaymentDate, request.PaymentMethod);
            if (@payment.IsSuccess)
            {
                @payment.Value.AttachPaymentStatus("Pending");
                @payment.Value.AttachPaymentDate(DateTime.Now);
                @payment.Value.AttachPaymentMethod(request.PaymentMethod);
                @payment.Value.AttachCurrency(request.Currency);
                @payment.Value.AttachAmount(request.Amount);
                @payment.Value.AttachOrderId(request.OrderId);
                
                var result =paymentRepository.AddAsync(@payment.Value);
                var email = new Mail
                {
                    Body = $"Your payment of {request.Amount} is Pending",
                    To = "constantinana343@yahoo.com",
                    Subject = "Payment Pending"
                };
                try
                {
                    await emailService.SendEmailAsync(email);
                }
                catch(Exception ex)
                {
                    logger.LogError($"Mailing about payment failed due to an error with the mail service: {ex.Message}");
                    return new CreatePaymentCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { "Payment failed due to an error with the mail service" }
                    };
                }
                return new CreatePaymentCommandResponse
                {
                    Success = true,
                    Payment = new CreatePaymentDto
                    {
                        PaymentId = @payment.Value.PaymentId,
                        OrderId = @payment.Value.OrderId,
                        Amount = @payment.Value.Amount,
                        PaymentDate = @payment.Value.PaymentDate,
                        PaymentMethod = @payment.Value.PaymentMethod,
                        Currency = @payment.Value.Currency,
                        PaymentStatus = @payment.Value.PaymentStatus,
                    }
                };
            }
            return new CreatePaymentCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { @payment.Error }
            };
        }
    }
}
