using MediatR;

namespace ECommerceApplication.Application.Features.OrderItems.Queries.GetByIdOrderItem
{
    public class GetByIdOrderItemQuery : IRequest<OrderItemDto>
    {
        public Guid OrderItemId { get; set; } = default!;
    }
}
