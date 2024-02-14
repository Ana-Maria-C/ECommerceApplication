using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, UpdateOrderItemCommandResponse>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        public UpdateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IProductRepository productRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        public async Task<UpdateOrderItemCommandResponse> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOrderItemCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                return new UpdateOrderItemCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var orderItem = await _orderItemRepository.FindByIdAsync(request.OrderItemId);
            if (orderItem == null)
                return new UpdateOrderItemCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            if (product == null)
                return new UpdateOrderItemCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            
            orderItem.Value.Update(request.ProductId, request.Quantity, request.PricePerUnit);

            await _orderItemRepository.UpdateAsync(orderItem.Value);
            return new UpdateOrderItemCommandResponse
            {
                Success = true,
                OrderItem = new UpdateOrderItemDto
                {
                    OrderItemId = orderItem.Value.OrderItemId,
                    ProductId = orderItem.Value.ProductId,
                    Quantity = orderItem.Value.Quantity,
                    PricePerUnit = orderItem.Value.PricePerUnit
                }
            };
        }
    }
}