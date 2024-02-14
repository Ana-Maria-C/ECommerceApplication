using MediatR;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Queries.GetAll
{
    public class GetAllShoppingCartsQuery : IRequest<GetAllShoppingCartsResponse>
    {
        public GetAllShoppingCartsQuery() { }

    }
}
