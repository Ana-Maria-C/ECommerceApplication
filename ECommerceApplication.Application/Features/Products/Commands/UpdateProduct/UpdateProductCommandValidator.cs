using ECommerceApplication.Application.Persistence;
using FluentValidation;

namespace ECommerceApplication.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductRepository repository;

        public UpdateProductCommandValidator(IProductRepository repository)
        {
            RuleFor(p => p.ProductName)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 100 characters.");
            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than zero.");
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed 1000 characters.");
            RuleFor(p => p.ImageUrl)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("{PropertyName} must not exceed 450 characters.");
            this.repository = repository;
        }
    }
}
