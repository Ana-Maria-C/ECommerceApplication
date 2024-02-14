using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductDto>
    {
        private readonly IProductRepository productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<UpdateProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator(productRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                return new UpdateProductDto
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var @event = await productRepository.FindByIdAsync(request.ProductId);
            if (@event == null)
            {
                return new UpdateProductDto
                {
                    Success = false,
                    ValidationsErrors = ["Product not found"]
                };
            }
            @event.Value.Update(request.ProductName, request.Price, request.Description, request.ImageUrl, request.CategoryId);
            await productRepository.UpdateAsync(@event.Value);

            return new UpdateProductDto
            {
                Success = true,
                ProductId = @event.Value.ProductId,
                CompanyId = @event.Value.CompanyId,
                ProductName = @event.Value.ProductName,
                Price = @event.Value.Price,
                Description = @event.Value.Description,
                ImageUrl = @event.Value.ImageUrl,
                CategoryId = @event.Value.CategoryId
            };
        }
    }
}