namespace ECommerceApplication.Application.Features.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemDto
    {
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
