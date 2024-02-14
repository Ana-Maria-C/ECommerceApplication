using ECommerceApplication.Application.Responses;


namespace ECommerceApplication.Application.Features.OrderItems.Queries.GetByFilterOrderItem
{
    public class GetByFilterOrderItemResponse : BaseResponse
    {
        public GetByFilterOrderItemResponse() : base()
        {
        }
        public List<OrderItemDto> Data { get; set; }
    }
}
