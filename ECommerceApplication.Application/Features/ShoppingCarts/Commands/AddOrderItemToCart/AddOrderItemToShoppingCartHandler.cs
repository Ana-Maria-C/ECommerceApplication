using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.AddOrderItemToCart
{
    public class AddOrderItemToShoppingCartHandler: IRequestHandler<AddOrderItemToShoppingCartCommand, AddOrderItemToShoppingCartResponse>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public AddOrderItemToShoppingCartHandler(IShoppingCartRepository shoppingCartRepository, IOrderItemRepository orderItemRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<AddOrderItemToShoppingCartResponse> Handle(AddOrderItemToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _shoppingCartRepository.FindByIdAsync(request.ShoppingCartId);

            if (shoppingCart == null)
            {
                return new AddOrderItemToShoppingCartResponse()
                {
                    Success = false,
                    Message = $"Shopping cart with id {request.ShoppingCartId} not found"
                };
            }

            var orderItem = await _orderItemRepository.FindByIdAsync(request.OrderItemId);

            if (orderItem == null)
            {
                return new AddOrderItemToShoppingCartResponse()
                {
                    Success = false,
                    Message = $"Order item with id {request.OrderItemId} not found"
                };
            }

            shoppingCart.Value.AddProduct(orderItem.Value);

            await _shoppingCartRepository.UpdateAsync(shoppingCart.Value);

            return new AddOrderItemToShoppingCartResponse()
            {
                Success = true,
                Message = $"Order item with id {request.OrderItemId} ADDED TO shopping cart with id {request.ShoppingCartId}"
            };
        }   
    }
}
