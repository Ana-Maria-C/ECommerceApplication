using Xunit;
using NSubstitute;
using ECommerceApplication.Application.Features.OrderItems.Commands.UpdateOrderItem;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Application.Tests.Commands.OrderItemTests
{
    public class UpdateOrderItemCommandHandlerTests
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly UpdateOrderItemCommandHandler _handler;

        public UpdateOrderItemCommandHandlerTests()
        {
            _orderItemRepository = Substitute.For<IOrderItemRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new UpdateOrderItemCommandHandler(_orderItemRepository, _productRepository);
        }

        [Fact]
        public async Task Handle_InvalidInput_ReturnsFailure()
        {
            // Arrange
            var invalidCommand = new UpdateOrderItemCommand { OrderItemId = Guid.Empty, ProductId = Guid.Empty, Quantity = 0, PricePerUnit = -1 };

            // Act
            var response = await _handler.Handle(invalidCommand, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.NotEmpty(response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_OrderItemNotFound_ReturnsFailure()
        {
            // Arrange
            var command = new UpdateOrderItemCommand { OrderItemId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 1, PricePerUnit = 10.0M };
            _orderItemRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult<Result<OrderItem>>(Result<OrderItem>.Failure($"Entity with id not found")));

            // Act
            var response = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.Contains("Entity with id not found", response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_ProductNotFound_ReturnsFailure()
        {
            // Arrange
            var command = new UpdateOrderItemCommand { OrderItemId = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 1, PricePerUnit = 10.0M };
            _orderItemRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult<Result<OrderItem>>(Result<OrderItem>.Success(OrderItem.Create(Guid.NewGuid(), 10, 10).Value)));
            _productRepository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromResult<Result<Product>>(Result<Product>.Failure($"Product with id not found")));

            // Act
            var response = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.Contains("Product with id not found", response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var orderItemId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var command = new UpdateOrderItemCommand { OrderItemId = orderItemId, ProductId = Guid.NewGuid(), Quantity = 1, PricePerUnit = 10.0M };
            _orderItemRepository.FindByIdAsync(orderItemId)
                .Returns(Task.FromResult<Result<OrderItem>>(Result<OrderItem>.Success(OrderItem.Create(Guid.NewGuid(), 10, 10).Value)));
            _productRepository.FindByIdAsync(command.ProductId)
                .Returns(Task.FromResult<Result<Product>>(Result<Product>.Success(Product.Create(companyId, "Iphone 15", 100).Value)));
            _orderItemRepository.UpdateAsync(Arg.Any<OrderItem>())
                .Returns(Task.FromResult<Result<OrderItem>>(Result<OrderItem>.Success((OrderItem.Create(Guid.NewGuid(), 10, 10).Value))));

            // Act
            var response = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response.OrderItem);
            Assert.Equal(orderItemId, response.OrderItem.OrderItemId);
            Assert.Equal(command.ProductId, response.OrderItem.ProductId);
            Assert.Equal(command.Quantity, response.OrderItem.Quantity);
            Assert.Equal(command.PricePerUnit, response.OrderItem.PricePerUnit);
        }

        [Fact]
        public async Task Handle_UpdateFailure_ReturnsFailure()
        {
            // Arrange
            var orderItemId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var command = new UpdateOrderItemCommand { OrderItemId = orderItemId, ProductId = Guid.NewGuid(), Quantity = 1, PricePerUnit = -1 };
            _orderItemRepository.FindByIdAsync(orderItemId)
                .Returns(Task.FromResult<Result<OrderItem>>(Result<OrderItem>.Success(OrderItem.Create(Guid.NewGuid(), 10, 10).Value)));
            _productRepository.FindByIdAsync(command.ProductId)
                .Returns(Task.FromResult<Result<Product>>(Result<Product>.Success(Product.Create(companyId, "Iphone 15", 100).Value)));
            _orderItemRepository.UpdateAsync(Arg.Any<OrderItem>())
                .Returns(Task.FromResult<Result<OrderItem>>(Result<OrderItem>.Failure("Update failed")));

            // Act
            var response = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.Contains("Update failed", response.ValidationsErrors);
        }
    }
}
