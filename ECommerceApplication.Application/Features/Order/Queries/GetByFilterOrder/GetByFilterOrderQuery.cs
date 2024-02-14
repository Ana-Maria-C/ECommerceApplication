using MediatR;

namespace ECommerceApplication.Application.Features.Order.Queries.GetByFilterOrder
{
    public class GetByFilterOrderQuery : IRequest<GetByFilterOrderResponse>
    {
        public Guid? UserId { get; set; } = default!;
    }
}
