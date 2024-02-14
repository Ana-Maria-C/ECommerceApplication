using ECommerceApplication.Application.Features.Products.Commands.CreateProduct;
using ECommerceApplication.Application.Persistence;
//using ECommerceApplication.Domain.Entities.Order;
using MediatR;
using ECommerceApplication.Domain;
using ECommerceApplication.Domain.Entities;
using ECommerceApplication.Application.Features.OrderItems;
using ECommerceApplication.Application.Features.Users.Queries;

namespace ECommerceApplication.Application.Features.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IOrderItemRepository orderItemRepository;

        public CreateOrderCommandHandler(IOrderRepository repository, IProductRepository productRepository, IAddressRepository addressRepository, IShoppingCartRepository shoppingCartRepository, IOrderItemRepository orderItemRepository)
        {
            this.orderRepository = repository;
            this.productRepository = productRepository;
            this.addressRepository = addressRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateOrderCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var shoppingCartExists = await shoppingCartRepository.FindByIdAsync(request.ShoppingCartId);
            if (!shoppingCartExists.IsSuccess )
            {
                return new CreateOrderCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Shopping Cart with the provided ID does not exist." }
                };
            }
            shoppingCartExists.Value.MarkAsUsed();

            var result = await shoppingCartRepository.AddAsync(ShoppingCart.Create(shoppingCartExists.Value.UserId).Value);


            var orderItemsResult = await orderItemRepository.getOrderItemsByFilter(request.ShoppingCartId, null);

            if (!orderItemsResult.IsSuccess) {
                return new CreateOrderCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { "Coud not get the orderItems from the shopping cart" }
                };
            }

            var orderItems = orderItemsResult.Value.ToList();

            var order = Domain.Entities.Order.Create(orderItems, shoppingCartExists.Value.UserId, request.AddressId);
            if (!order.IsSuccess)
            {
                return new CreateOrderCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { order.Error }
                };

            }

            await orderRepository.AddAsync(order.Value);

            return new CreateOrderCommandResponse
            {
                Success = true,
                Data = new CreateOrderDto
                {
                    OrderId = order.Value.OrderId,
                    UserId = order.Value.UserId,
                    OrderItems = order.Value.OrderItems.Select(o => new OrderItemDto
                    {
                        OrderItemId = o.OrderItemId,
                        ProductId = o.ProductId,
                        Quantity = o.Quantity,
                        PricePerUnit = o.PricePerUnit
                    }).ToList()
                }
            };
        }
    }
}
