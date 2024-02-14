namespace ECommerceApplication.App.ViewModels
{
    public class ShoppingCartDto
    {
        public Guid ShoppingCartId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

    }
}
