using MediatR;

namespace ECommerceApplication.Application.Features.OrderItems.Queries.GetAllOrderItem
{
    public class GetAllOrderItemQuery : IRequest<GetAllOrderItemResponse>
    {
        public GetAllOrderItemQuery() { }

    }
}
