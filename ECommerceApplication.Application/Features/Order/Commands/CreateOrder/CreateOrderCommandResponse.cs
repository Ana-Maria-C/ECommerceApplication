using ECommerceApplication.Application.Features.OrderItems.Commands.CreateOrderItem;
using ECommerceApplication.Application.Responses;

namespace ECommerceApplication.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandResponse : BaseResponse
    {
        public CreateOrderCommandResponse() : base()
        {
        }

        public CreateOrderDto Data { get; set; }
    }
}