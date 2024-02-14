using ECommerceApplication.App.ViewModels;

namespace ECommerceApplication.App.Contracts
{
    public interface IOrderDataService
    {
        Task<OrderViewModel> Create(Guid shoppingCartId, Guid addressId);
    }
}
