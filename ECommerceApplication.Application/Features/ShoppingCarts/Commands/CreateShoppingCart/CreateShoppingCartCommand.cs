using MediatR;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.CreateShoppingCart
{
    public class CreateShoppingCartCommand : IRequest<CreateShoppingCartCommandResponse>
    {
        public Guid UserId { get; set; } = default!;
    }
}
