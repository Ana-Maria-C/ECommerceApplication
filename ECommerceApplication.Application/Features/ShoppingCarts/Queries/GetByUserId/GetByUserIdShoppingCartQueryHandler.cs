using ECommerceApplication.Application.Features.Products.Queries.GetByIdProduct;
using ECommerceApplication.Application.Features.ShoppingCarts.Queries;
using ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetAll;
using ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetByUserIdShoppingCart;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using MediatR;
using Stripe;

namespace ECommerceApplication.Application.Features.Categories.Queries.GetByUserIdShoppingCart
{
    public class GetByUserIdShoppingCartQueryHandler : IRequestHandler<GetByUserIdShoppingCartQuery, GetByUserIdShoppingCartResponse>
    {
        private readonly IShoppingCartRepository repository;

        public GetByUserIdShoppingCartQueryHandler(IShoppingCartRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetByUserIdShoppingCartResponse> Handle(GetByUserIdShoppingCartQuery request, CancellationToken cancellationToken)
        {
            GetByUserIdShoppingCartResponse response = new();
            var result = await repository.FindByUserIdAsync(request.UserId);
            if (!result.IsSuccess)
            {
                response.Success = false;
                response.ValidationsErrors = [result.Error];
                return response;
            }
            return new GetByUserIdShoppingCartResponse
            {
                Success = true,
                Data = new ShoppingCartDto
                {
                    UserId = result.Value.UserId,
                    ShoppingCartId = result.Value.ShoppingCartId,
                    OrderItems = result.Value.OrderItems,
                    Total = result.Value.CalculateTotal()
                }
            };
        }
    }
}
