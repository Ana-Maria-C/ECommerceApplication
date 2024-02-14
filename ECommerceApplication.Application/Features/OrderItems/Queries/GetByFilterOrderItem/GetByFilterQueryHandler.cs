using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using MediatR;

namespace ECommerceApplication.Application.Features.OrderItems.Queries.GetByFilterOrderItem
{
    public class GetByIdOrderItemQueryHandler : IRequestHandler<GetByFilterOrderItemQuery, GetByFilterOrderItemResponse>
    {
        private readonly IOrderItemRepository repository;

        public GetByIdOrderItemQueryHandler(IOrderItemRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetByFilterOrderItemResponse> Handle(GetByFilterOrderItemQuery request, CancellationToken cancellationToken)
        {
            GetByFilterOrderItemResponse response = new();
            var result = await repository.getOrderItemsByFilter(request.ShoppingCartId, request.ProductId);
            if (!result.IsSuccess)
            {
                response.Success = false;
                response.ValidationsErrors = [result.Error];
                return response;
            }
            return new GetByFilterOrderItemResponse
            {
                Success = true,
                Data = result.Value.Select(o => new OrderItemDto {
                    OrderItemId = o.OrderItemId,
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                    PricePerUnit = o.PricePerUnit
                }).ToList()
            };
        }
    }
}
