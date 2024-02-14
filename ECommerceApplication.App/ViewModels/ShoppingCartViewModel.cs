namespace ECommerceApplication.App.ViewModels
{
    public class ShoppingCartViewModel
    {
        public Guid ShoppingCartId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();  
        public decimal Total { get; set; }
    }
}
