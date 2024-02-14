namespace ECommerceApplication.App.ViewModels
{
    public class CreateOrderDto
    {
        public Guid ShoppingCartId { get; set; }
        public Guid AddressId { get; set; }
    }
}
