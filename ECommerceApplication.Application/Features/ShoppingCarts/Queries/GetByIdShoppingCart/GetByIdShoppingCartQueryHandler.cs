using ECommerceApplication.Application.Features.ShoppingCarts.Queries;
using ECommerceApplication.Application.Persistence;
using MediatR;

namespace ECommerceApplication.Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdShoppingCartQueryHandler : IRequestHandler<GetByIdShoppingCartQuery, ShoppingCartDto>
    {
        private readonly IShoppingCartRepository repository;

        public GetByIdShoppingCartQueryHandler(IShoppingCartRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ShoppingCartDto> Handle(GetByIdShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await repository.FindByIdAsync(request.ShoppingCartId);
            if (shoppingCart.IsSuccess)
            {
                return new ShoppingCartDto
                {
                    UserId = shoppingCart.Value.UserId,
                    ShoppingCartId = shoppingCart.Value.ShoppingCartId,
                    OrderItems = shoppingCart.Value.OrderItems
                };
            }
            return new ShoppingCartDto();
        }
    }
}
