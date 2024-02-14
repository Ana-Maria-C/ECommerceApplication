using ECommerceApplication.Application.Persistence;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.RemoveOrderItemFromCart
{
    public class RemoveOrderItemFromShoppingCartHandler
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public RemoveOrderItemFromShoppingCartHandler(IShoppingCartRepository shoppingCartRepository, IOrderItemRepository orderItemRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<RemoveOrderItemFromShoppingCartResponse> Handle(RemoveOrderItemFromShoppingCartCommand request)
        {
            var shoppingCart = await _shoppingCartRepository.FindByIdAsync(request.ShoppingCartId);

            if (shoppingCart == null)
            {
                return new RemoveOrderItemFromShoppingCartResponse()
                {
                    Success = false,
                    Message = $"Shopping cart with id {request.ShoppingCartId} not found"
                };
            }

            var orderItem = await _orderItemRepository.FindByIdAsync(request.OrderItemId);

            if (orderItem == null)
            {
                return new RemoveOrderItemFromShoppingCartResponse()
                {
                    Success = false,
                    Message = $"Order item with id {request.OrderItemId} not found"
                };
            }

            shoppingCart.Value.RemoveProduct(request.OrderItemId);

            await _shoppingCartRepository.UpdateAsync(shoppingCart.Value);

            return new RemoveOrderItemFromShoppingCartResponse()
            {
                Success = true,
                Message = $"Order item with id {request.OrderItemId} removed from shopping cart with id {request.ShoppingCartId}"
            };
        }   
    }
}
