using ECommerceApplication.Application.Features.Order;
using ECommerceApplication.Application.Features.Order.Queries.GetByIdOrder;
using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Order.Queries.GetByIdOrder
{   
    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQuery, OrderDto>
    {
        private readonly IOrderRepository repository;

        public GetByIdOrderQueryHandler(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public async Task<OrderDto> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
        {
            var orderItem = await repository.FindByIdAsync(request.OrderId);
            if (orderItem.IsSuccess)
            {
                return new OrderDto
                {
                    OrderId = orderItem.Value.OrderId,
                    UserId = orderItem.Value.UserId,
                    OrderPaid = orderItem.Value.OrderPaid,
                    Payment = orderItem.Value.Payment,
                    AddressId = orderItem.Value.AddressId,
                    Address = orderItem.Value.Address
                };
            }
            return new OrderDto();
        }
    }
}
