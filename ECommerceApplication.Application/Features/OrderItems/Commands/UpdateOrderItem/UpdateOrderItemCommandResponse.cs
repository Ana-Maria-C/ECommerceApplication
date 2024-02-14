using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandResponse : BaseResponse
    {
        public UpdateOrderItemCommandResponse() : base()
        {
        }
        public UpdateOrderItemDto OrderItem { get; set; }
    }
}
