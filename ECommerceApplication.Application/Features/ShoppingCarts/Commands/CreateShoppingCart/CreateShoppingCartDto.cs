namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.CreateShoppingCart
{
    public class CreateShoppingCartDto
    {
        public Guid ShoppingCartId { get; set; }
        public Guid UserId { get; set; }
    }
}
