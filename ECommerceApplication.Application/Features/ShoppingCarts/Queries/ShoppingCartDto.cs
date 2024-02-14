using ECommerceApplication.Domain.Entities;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Queries
{
    public class ShoppingCartDto
    {
        public Guid ShoppingCartId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        public Product? Product { get; set; }
        public decimal Total { get; set; }
        
    }
}
