using ECommerceApplication.Application.Features.OrderItems;
using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.OrderItems.Queries.GetAllOrderItem
{
    public class GetAllOrderItemQueryHandler : IRequestHandler<GetAllOrderItemQuery, GetAllOrderItemResponse>
    {
        private readonly IOrderItemRepository orderItemRepository;

        public GetAllOrderItemQueryHandler(IOrderItemRepository orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }

        public async Task<GetAllOrderItemResponse> Handle(GetAllOrderItemQuery request, CancellationToken cancellationToken)
        {
            GetAllOrderItemResponse response = new();
            var result = await orderItemRepository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.OrderItems = result.Value.Select(o => new OrderItemDto
                {
                    OrderItemId = o.OrderItemId,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                    PricePerUnit = o.PricePerUnit,
                    ShoppingCartId = o.ShoppingCartId
                }).ToList();
            }
            return response;
        }
    }
}
