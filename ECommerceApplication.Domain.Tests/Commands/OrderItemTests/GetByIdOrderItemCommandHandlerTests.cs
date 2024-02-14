/*using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerceApplication.Application.Features.OrderItems;
using ECommerceApplication.Application.Features.OrderItems.Queries.GetByFilterOrderItem;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;
using NSubstitute;
using Xunit;
using GetByIdOrderItemQueryHandler = ECommerceApplication.Application.Features.OrderItems.Queries.GetByIdOrderItem.GetByIdOrderItemQueryHandler;

namespace ECommercerApplication.Application.Tests.Queries.OrderItemTests
{
    public class GetByIdOrderItemQueryHandlerTests
    {
        private readonly IOrderItemRepository _repository;
        private readonly GetByIdOrderItemQueryHandler _handler;

        public GetByIdOrderItemQueryHandlerTests()
        {
            _repository = Substitute.For<IOrderItemRepository>();
            _handler = new GetByIdOrderItemQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_OrderItemFound_ReturnsOrderItemDto()
        {
            // Arrange
            var orderItemId = Guid.NewGuid();
            var mockOrderItem = OrderItem.Create(Guid.NewGuid(), 2, 10.99m);

            _repository.FindByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(Result<OrderItem>.Success(mockOrderItem.Value)));

            var query = new GetByFilterOrderItemQuery { OrderItemId = orderItemId };

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockOrderItem.Value.OrderItemId, result.OrderItemId);
            Assert.Equal(mockOrderItem.Value.ProductId, result.ProductId);
            Assert.Equal(mockOrderItem.Value.Quantity, result.Quantity);
            Assert.Equal(mockOrderItem.Value.PricePerUnit, result.PricePerUnit);
        }

        [Fact]
        public async Task Handle_OrderItemNotFound_ReturnsEmptyDto()
        {
            // Arrange
            var orderItemId = Guid.NewGuid();
            _repository.FindByIdAsync(orderItemId).Returns(Task.FromResult(Result<OrderItem>.Failure("Not found")));

            //var query = new GetByFilterOrderItemQuery { OrderItemId = orderItemId };

            // Act
            //var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            *//*Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.OrderItemId);
            Assert.Equal(Guid.Empty, result.ProductId);
            Assert.Equal(0, result.Quantity);
            Assert.Equal(0m, result.PricePerUnit);*//*
        }

        // Additional test cases if any specific edge cases exist...
    }
}
*/