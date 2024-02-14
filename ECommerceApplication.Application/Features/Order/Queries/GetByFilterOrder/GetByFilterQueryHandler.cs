using ECommerceApplication.Application.Features.OrderItems;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using MediatR;

namespace ECommerceApplication.Application.Features.Order.Queries.GetByFilterOrder
{
    public class GetByIdOrderQueryHandler : IRequestHandler<GetByFilterOrderQuery, GetByFilterOrderResponse>
    {
        private readonly IOrderRepository orderRepository;

        public GetByIdOrderQueryHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<GetByFilterOrderResponse> Handle(GetByFilterOrderQuery request, CancellationToken cancellationToken)
        {
            GetByFilterOrderResponse response = new();
            var result = await orderRepository.getOrdersByFilter(request.UserId);
            if (!result.IsSuccess)
            {
                response.Success = false;
                response.ValidationsErrors = [result.Error];
                return response;
            }
            return new GetByFilterOrderResponse
            {
                Success = true,
                Data = result.Value.Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    OrderPaid = o.OrderPaid,
                    Payment = o.Payment,
                    AddressId = o.AddressId,
                    Address = o.Address,
                    OrderItems = o.OrderItems
                }).ToList()
            };
        }
    }
}
