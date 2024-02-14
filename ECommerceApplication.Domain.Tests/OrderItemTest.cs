using ECommerceApplication.Domain.Entities;
using FluentAssertions;

namespace ECommerceApplication.Domain.Tests
{
    public class OrderItemTests
    {
        [Fact]
        public void When_CreateOrderItemIsCalled_WithValidParameters_Then_SuccessIsReturned()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            var result = OrderItem.Create(productId, 2, 10.0m);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.OrderItemId.Should().NotBe(Guid.Empty);
            result.Value.ProductId.Should().Be(productId);
            result.Value.Quantity.Should().Be(2);
            result.Value.PricePerUnit.Should().Be(10.0m);
        }

        [Fact]
        public void When_CreateOrderItemIsCalled_WithInvalidProductId_Then_FailureIsReturned()
        {
            // Act
            var result = OrderItem.Create(Guid.Empty, 2, 10.0m);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Product id is required.");
        }

        [Fact]
        public void When_CreateOrderItemIsCalled_WithInvalidQuantity_Then_FailureIsReturned()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            var result = OrderItem.Create(productId, 0, 10.0m);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Quantity must be greater than zero.");
        }

        [Fact]
        public void When_CreateOrderItemIsCalled_WithInvalidPricePerUnit_Then_FailureIsReturned()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            var result = OrderItem.Create(productId, 2, 0.0m);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Price per unit must be greater than zero.");
        }

        [Fact]
        public void When_UpdateOrderItemIsCalled_WithValidParameters_Then_OrderItemIsUpdatedSuccessfully()
        {
            // Arrange
            var orderItem = OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value;
            var newProductId = Guid.NewGuid();

            // Act
            orderItem.Update(newProductId, 3, 15.0m);

            // Assert
            orderItem.ProductId.Should().Be(newProductId);
            orderItem.Quantity.Should().Be(3);
            orderItem.PricePerUnit.Should().Be(15.0m);
        }

        [Fact]
        public void When_CalculateTotalIsCalled_Then_TotalIsCalculatedCorrectly()
        {
            // Arrange
            var orderItem = OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value;

            // Act
            var total = orderItem.CalculateTotal();

            // Assert
            total.Should().Be(20.0m);
        }
    }
}
