using ECommerceApplication.App.ViewModels;

namespace ECommerceApplication.App.Contracts
{
    public interface IShoppingCartDataService
    {
        Task<List<ShoppingCartViewModel>> GetShoppingCartsAsync();
        Task<ShoppingCartViewModel> GetShoppingCartByIdAsync(Guid shoppingCartId);

        Task<ShoppingCartViewModel> GetShoppingCartByUserIdAsync(Guid userId);

        Task<ShoppingCartViewModel> AttachOrderItemById(Guid shoppingCartId, OrderItemDto orderItemId);
    }
}