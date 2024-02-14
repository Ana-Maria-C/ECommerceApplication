using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.OrderItems.Commands.DeleteCategory
{
    public class DeleteOrderItemQueryHandler : IRequestHandler <DeleteOrderItemQuery, DeleteOrderItemResponse>
    {
       private readonly IOrderItemRepository _orderItemRepository;
        public DeleteOrderItemQueryHandler(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
        public async Task<DeleteOrderItemResponse> Handle(DeleteOrderItemQuery request, CancellationToken cancellationToken)
        {
            var response = new DeleteOrderItemResponse();
            var orderItem = await _orderItemRepository.FindByIdAsync(request.OrderItemId);
            if (orderItem == null)
            {
                response.Success = false;
                response.ValidationsErrors = new List<string> { "OrderItem not found" };
                return response;
            }
            await _orderItemRepository.DeleteAsync(orderItem.Value.OrderItemId);
            response.Data = new OrderItemDto
            {
                OrderItemId = orderItem.Value.OrderItemId,
                ProductId = orderItem.Value.ProductId,
                Quantity = orderItem.Value.Quantity,
                PricePerUnit = orderItem.Value.PricePerUnit
            };
            response.Success = true;
            response.Message = "OrderItem deleted successfully";
            return response;

        }
    }
}
