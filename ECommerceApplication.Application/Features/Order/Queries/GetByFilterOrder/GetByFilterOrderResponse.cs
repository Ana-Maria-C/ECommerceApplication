using ECommerceApplication.Application.Features.OrderItems;
using ECommerceApplication.Application.Responses;


namespace ECommerceApplication.Application.Features.Order.Queries.GetByFilterOrder
{
    public class GetByFilterOrderResponse : BaseResponse
    {
        public GetByFilterOrderResponse() : base()
        {
        }
        public List<OrderDto> Data { get; set; }
    }
}
