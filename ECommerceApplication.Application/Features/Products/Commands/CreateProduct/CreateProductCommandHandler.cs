using ECommerceApplication.Application.Contracts;
using ECommerceApplication.Application.Features.Products.Queries;
using ECommerceApplication.Application.Models;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApplication.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IEmailService emailService;
        private readonly ILogger<CreateProductCommandHandler> logger;

        public CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IEmailService emailService, ILogger<CreateProductCommandHandler> logger)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.emailService = emailService;
            this.logger = logger;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateProductCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var categoryExists = await categoryRepository.CategoryExists(request.CategoryId);
            if (!categoryExists)
            {
                return new CreateProductCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Category with the provided ID does not exist." }
                };
            }
            var @event = Product.Create(request.CompanyId, request.ProductName, request.Price);
            if (@event.IsSuccess)
            {
                var category = await categoryRepository.FindByIdAsync(request.CategoryId);

                @event.Value.AttachCategory(category.Value);

#pragma warning disable CS8604 // Possible null reference argument.
                @event.Value.AttachDescription(request.Description);
#pragma warning restore CS8604 // Possible null reference argument.


#pragma warning disable CS8604 // Possible null reference argument.
                @event.Value.AttachImageUrl(request.ImageUrl);
#pragma warning restore CS8604 // Possible null reference argument.

#pragma warning disable CS8604 // Possible null reference argument.
                //@event.Value.AttachManufacturer(request.ManufacturerId);
#pragma warning restore CS8604 // Possible null reference argument.

                await productRepository.AddAsync(@event.Value);
                var email = new Mail
                {
                    Body = $"A new event with name:{@event.Value.ProductName} and price: {@event.Value.Price} has been created",
                    // don't forget to change the email address
                    To = "alex_robert02@yahoo.com",
                    Subject = "New Product created",
                };

                try
                {
                    await emailService.SendEmailAsync(email);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Email sending failed");
                    return new CreateProductCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { "Email sending failed" }
                    };
                }

                return new CreateProductCommandResponse
                {
                    Success = true,
                    Product = new ProductDto
                    {
                        ProductId = @event.Value.ProductId,
                        CompanyId = @event.Value.CompanyId,
                        ProductName = @event.Value.ProductName,
                        Price = @event.Value.Price,
                        Description = @event.Value.Description,
                        ImageUrl = @event.Value.ImageUrl,
                        Category = new CategoryDto
                        {
                            CategoryId = @event.Value.CategoryId,
                        }

                    }
                };


            }

            return new CreateProductCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { @event.Error }
            };
        }
    }
}
