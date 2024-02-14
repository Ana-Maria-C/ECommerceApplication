using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerceApplication.Application.Features.OrderItems;
using ECommerceApplication.Application.Features.OrderItems.Queries.GetAllOrderItem;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;
using NSubstitute;
using Xunit;

namespace ECommercerApplication.Application.Tests.Queries.OrderItemTests
{
    public class GetAllOrderItemQueryHandlerTests
    {
        private readonly IOrderItemRepository _repository;
        private readonly GetAllOrderItemQueryHandler _handler;

        public GetAllOrderItemQueryHandlerTests()
        {
            _repository = Substitute.For<IOrderItemRepository>();
            _handler = new GetAllOrderItemQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_ReturnsAllOrderItems()
        {
            // Arrange
            var orderItems = new List<OrderItem>
            {
                OrderItem.Create(Guid.NewGuid(), 2, 10.99m ).Value,
                OrderItem.Create(Guid.NewGuid(), 3, 15.99m).Value              // Add more order items as needed
            };

            var result = Result<IReadOnlyList<OrderItem>>.Success(orderItems);
            _repository.GetAllAsync().Returns(Task.FromResult<Result<IReadOnlyList<OrderItem>>>(result));

            // Act
            var response = await _handler.Handle(new GetAllOrderItemQuery(), new CancellationToken());

            // Assert
            Assert.NotNull(response.OrderItems);
            Assert.Equal(orderItems.Count, response.OrderItems.Count);
            Assert.All(response.OrderItems, orderItemDto =>
                Assert.Contains(orderItems, o => o.OrderItemId == orderItemDto.OrderItemId &&
                                                 o.ProductId == orderItemDto.ProductId &&
                                                 o.Quantity == orderItemDto.Quantity &&
                                                 o.PricePerUnit == orderItemDto.PricePerUnit));
        }

        // Additional test cases as needed...
    }
}
