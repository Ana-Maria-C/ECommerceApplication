using ECommerceApplication.Domain.Entities;
using FluentAssertions;

namespace ECommerceApplication.Domain.Tests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void When_CreateShoppingCartIsCalled_WithValidParameters_Then_SuccessIsReturned()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var result = ShoppingCart.Create(userId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.ShoppingCartId.Should().NotBe(Guid.Empty);
            result.Value.UserId.Should().Be(userId);
            result.Value.OrderItems.Should().BeEmpty();
        }

        [Fact]
        public void When_CreateShoppingCartIsCalled_WithInvalidUserId_Then_FailureIsReturned()
        {
            // Act
            var result = ShoppingCart.Create(Guid.Empty);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("User id is required.");
        }

        [Fact]
        public void When_AddProductIsCalled_Then_ProductIsAddedToShoppingCart()
        {
            // Arrange
            var shoppingCart = ShoppingCart.Create(Guid.NewGuid()).Value;
            var orderItem = OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value;

            // Act
            shoppingCart.AddProduct(orderItem);

            // Assert
            shoppingCart.OrderItems.Should().Contain(orderItem);
        }

        [Fact]
        public void When_RemoveProductIsCalled_WithValidOrderItemId_Then_ProductIsRemovedFromShoppingCart()
        {
            // Arrange
            var shoppingCart = ShoppingCart.Create(Guid.NewGuid()).Value;
            var orderItem = OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value;
            shoppingCart.AddProduct(orderItem);

            // Act
            shoppingCart.RemoveProduct(orderItem.OrderItemId);

            // Assert
            shoppingCart.OrderItems.Should().NotContain(orderItem);
        }

        [Fact]
        public void When_RemoveProductIsCalled_WithInvalidOrderItemId_Then_ShoppingCartIsUnchanged()
        {
            // Arrange
            var shoppingCart = ShoppingCart.Create(Guid.NewGuid()).Value;
            var orderItem = OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value;
            shoppingCart.AddProduct(orderItem);

            // Act
            shoppingCart.RemoveProduct(Guid.Empty);

            // Assert
            shoppingCart.OrderItems.Should().Contain(orderItem);
        }

        [Fact]
        public void When_CalculateTotalIsCalled_Then_TotalIsCalculatedCorrectly()
        {
            // Arrange
            var shoppingCart = ShoppingCart.Create(Guid.NewGuid()).Value;
            var orderItem1 = OrderItem.Create(Guid.NewGuid(), 1, 10.0m).Value;
            var orderItem2 = OrderItem.Create(Guid.NewGuid(), 2, 15.0m).Value;
            shoppingCart.AddProduct(orderItem1);
            shoppingCart.AddProduct(orderItem2);

            // Act
            var total = shoppingCart.CalculateTotal();

            // Assert
            total.Should().Be(40.0m);
        }
    }
}
