using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.OrderItems.Queries.GetByIdOrderItem
{
    public class GetByIdOrderItemQueryHandler : IRequestHandler<GetByIdOrderItemQuery, OrderItemDto>
    {
        private readonly IOrderItemRepository repository;

        public GetByIdOrderItemQueryHandler(IOrderItemRepository repository)
        {
            this.repository = repository;
        }

        public async Task<OrderItemDto> Handle(GetByIdOrderItemQuery request, CancellationToken cancellationToken)
        {
            var orderItem = await repository.FindByIdAsync(request.OrderItemId);
            if (orderItem.IsSuccess)
            {
                return new OrderItemDto
                {
                    OrderItemId = orderItem.Value.OrderItemId,
                    ProductId = orderItem.Value.ProductId,
                    Quantity = orderItem.Value.Quantity,
                    PricePerUnit = orderItem.Value.PricePerUnit
                };
            }
            return new OrderItemDto();
        }
    }
}
