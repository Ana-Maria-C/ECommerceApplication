using ECommerceApplication.Application.Responses;


namespace ECommerceApplication.Application.Features.OrderItems.Queries.GetAllOrderItem
{
    public class GetAllOrderItemResponse : BaseResponse
    {
        public GetAllOrderItemResponse() : base()
        {
        }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
