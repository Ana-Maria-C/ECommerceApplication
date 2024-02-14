using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetAll
{
    public class GetAllShoppingCartsQueryHandler : IRequestHandler<GetAllShoppingCartsQuery, GetAllShoppingCartsResponse>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetAllShoppingCartsQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<GetAllShoppingCartsResponse> Handle(GetAllShoppingCartsQuery request, CancellationToken cancellationToken)
        {
            GetAllShoppingCartsResponse response = new();
            var result = await _shoppingCartRepository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.ShoppingCarts = result.Value.Select(c => new ShoppingCartDto
                {
                    UserId = c.UserId,
                    ShoppingCartId = c.ShoppingCartId,
                    OrderItems = c.OrderItems
                }).ToList();
            }
            return response;
        }
    }
}
