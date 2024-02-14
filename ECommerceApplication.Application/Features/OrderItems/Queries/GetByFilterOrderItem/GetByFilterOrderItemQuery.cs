using MediatR;

namespace ECommerceApplication.Application.Features.OrderItems.Queries.GetByFilterOrderItem
{
    public class GetByFilterOrderItemQuery : IRequest<GetByFilterOrderItemResponse>
    {
        public Guid OrderItemId { get; set; } = default!;

        public Guid? ProductId { get; set; } = default!;

        public Guid? ShoppingCartId { get; set; } = default!;
    }
}
