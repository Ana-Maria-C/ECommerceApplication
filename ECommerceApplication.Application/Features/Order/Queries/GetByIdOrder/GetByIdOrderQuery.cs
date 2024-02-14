using ECommerceApplication.Application.Features.OrderItems;
using MediatR;

namespace ECommerceApplication.Application.Features.Order.Queries.GetByIdOrder
{
    public class GetByIdOrderQuery : IRequest<OrderDto>
    {
        public Guid OrderId { get; set; } = default!;
    }
}
