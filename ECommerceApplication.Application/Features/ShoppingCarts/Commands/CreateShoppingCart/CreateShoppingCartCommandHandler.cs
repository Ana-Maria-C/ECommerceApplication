using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using MediatR;

namespace ECommerceApplication.Application.Features.ShoppingCarts.Commands.CreateShoppingCart
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, CreateShoppingCartCommandResponse>
    {
        private readonly IShoppingCartRepository repository;

        public CreateShoppingCartCommandHandler(IShoppingCartRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateShoppingCartCommandResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateShoppingCartCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateShoppingCartCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var shoppingCart = ShoppingCart.Create(request.UserId);
            if (!shoppingCart.IsSuccess)
            {
                return new CreateShoppingCartCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { shoppingCart.Error }
                };
            }

            await repository.AddAsync(shoppingCart.Value);

            return new CreateShoppingCartCommandResponse
            {
                Success = true,
                ShoppingCart = new CreateShoppingCartDto
                {
                    ShoppingCartId = shoppingCart.Value.ShoppingCartId,
                    UserId = shoppingCart.Value.UserId
                }
            };
        }
    }
}
