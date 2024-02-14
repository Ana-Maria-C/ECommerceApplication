using ECommerceApplication.Application.Features.Products.Commands.CreateProduct;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using MediatR;

namespace ECommerceApplication.Application.Features.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, CreateOrderItemCommandResponse>
    {
        private readonly IOrderItemRepository repository;
        private readonly IProductRepository productRepository;

        public CreateOrderItemCommandHandler(IOrderItemRepository repository, IProductRepository productRepository)
        {
            this.repository = repository;
            this.productRepository = productRepository;
        }

        public async Task<CreateOrderItemCommandResponse> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderItemCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateOrderItemCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var productExists = await productRepository.ProductExists(request.ProductId);
            if (!productExists)
            {
                return new CreateOrderItemCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Product with the provided ID does not exist." }
                };
            }

            var orderItem = OrderItem.Create(request.ProductId, request.Quantity, request.PricePerUnit);
            if (!orderItem.IsSuccess)
            {
                return new CreateOrderItemCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { orderItem.Error }
                };
            }

            await repository.AddAsync(orderItem.Value);

            return new CreateOrderItemCommandResponse
            {
                Success = true,
                Data = new CreateOrderItemDto
                {
                    OrderItemId = orderItem.Value.OrderItemId,
                    ProductId = orderItem.Value.ProductId,
                    Quantity = orderItem.Value.Quantity,
                    PricePerUnit =orderItem.Value.PricePerUnit

                }
            };
        }
    }
}
