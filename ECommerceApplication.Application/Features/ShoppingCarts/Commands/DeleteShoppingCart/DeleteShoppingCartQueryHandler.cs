using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.DeleteShoppingCart
{
    public class DeleteShoppingCartQueryHandler : IRequestHandler<DeleteShoppingCartQuery, DeleteShoppingCartResponse>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public DeleteShoppingCartQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<DeleteShoppingCartResponse> Handle(DeleteShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var response = new DeleteShoppingCartResponse();
            var shoppingCart = await _shoppingCartRepository.FindByIdAsync(request.ShoppingCartId);
            if (shoppingCart == null)
            {
                response.Success = false;
                response.ValidationsErrors = new List<string> { "ShoppingCart not found" };
                return response;
            }
            await _shoppingCartRepository.DeleteAsync(shoppingCart.Value.ShoppingCartId);
            response.Success = true;
            response.Message = "ShoppingCart deleted successfully";
            return response;

        }
    }
}
