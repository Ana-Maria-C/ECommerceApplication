// Import necessary namespaces

using ECommerceApplication.Domain.Entities;
using FluentAssertions;

namespace ECommerceApplication.Domain.Tests
{
    public class OrderTests
    {
        [Fact]
        public void When_CreateOrderIsCalled_WithValidParameters_Then_SuccessIsReturned()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var orderItems = new List<OrderItem>
            {
                OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value,
                OrderItem.Create(Guid.NewGuid(), 1, 15.0m).Value
            };

            var Order = new Order();

            // Act
            var result = Order.Create(orderItems, userId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.OrderId.Should().NotBe(Guid.Empty);
            result.Value.UserId.Should().Be(userId);
            result.Value.OrderItems.Should().BeEquivalentTo(orderItems);
            result.Value.OrderPaid.Should().BeFalse();
            result.Value.Payment.Should().BeNull();
        }

        [Fact]
        public void When_CreateOrderIsCalled_WithNoOrderItems_Then_FailureIsReturned()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var orderItems = new List<OrderItem>();

            // Act
            var result = Order.Create(orderItems, userId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Order items are required.");
        }

        [Fact]
        public void When_CreateOrderIsCalled_WithInvalidUserId_Then_FailureIsReturned()
        {
            // Arrange
            var userId = Guid.Empty;
            var orderItems = new List<OrderItem>
            {
                OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value
            };

            // Act
            var result = Order.Create(orderItems, userId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("User id should not be default");
        }

        [Fact]
        public void When_AddPaymentIsCalled_WithValidPayment_Then_PaymentIsAddedToOrder()
        {
            // Arrange
            var order = Order.Create(new List<OrderItem> { OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value }, Guid.NewGuid());
            var payment = Payment.Create(order.Value.OrderId, 30.0m, DateTime.UtcNow, "Card").Value;

            // Act
            order.Value.AddPayment(payment);

            // Assert
            order.Value.Payment.Should().Be(payment);
        }

        [Fact]
        public void When_MarkAsPaidIsCalled_WithValidPayment_Then_OrderIsMarkedAsPaid()
        {
            // Arrange
            var order = Order.Create(new List<OrderItem> { OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value }, Guid.NewGuid());
            var payment = Payment.Create(order.Value.OrderId, 20.0m, DateTime.UtcNow, "Card").Value;
            order.Value.AddPayment(payment);

            // Act
            order.Value.Payment.MarkAsPaid();
            order.Value.MarkAsPaid();

            // Assert
            order.Value.OrderPaid.Should().BeTrue();
        }

        [Fact]
        public void When_MarkAsPaidIsCalled_WithoutPayment_Then_OrderIsNotMarkedAsPaid()
        {
            // Arrange
            var order = Order.Create(new List<OrderItem> { OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value }, Guid.NewGuid());

            // Act
            order.Value.MarkAsPaid();

            // Assert
            order.Value.OrderPaid.Should().BeFalse();
        }

        [Fact]
        public void When_MarkAsPaidIsCalled_WithInvalidPayment_Then_OrderIsNotMarkedAsPaid()
        {
            // Arrange
            var order = Order.Create(new List<OrderItem> { OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value }, Guid.NewGuid());
            var invalidPayment = Payment.Create(order.Value.OrderId, 10.0m, DateTime.UtcNow, "InvalidCard").Value;
            order.Value.AddPayment(invalidPayment);

            // Act
            order.Value.MarkAsPaid();

            // Assert
            order.Value.OrderPaid.Should().BeFalse();
        }

        [Fact]
        public void When_CancelOrderIsCalled_ForUnpaidOrder_Then_NoExceptionIsThrown()
        {
            // Arrange
            var order = Order.Create(new List<OrderItem> { OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value }, Guid.NewGuid());

            // Act & Assert
            order.Invoking(o => o.Value.CancelOrder())
                .Should().NotThrow<InvalidOperationException>();
        }

        [Fact]
        public void When_CancelOrderIsCalled_ForPaidOrder_Then_ExceptionIsThrown()
        {
            // Arrange
            var order = Order.Create(new List<OrderItem> { OrderItem.Create(Guid.NewGuid(), 2, 10.0m).Value }, Guid.NewGuid());
            var payment = Payment.Create(order.Value.OrderId, 10.0m, DateTime.UtcNow, "Card").Value;
            order.Value.AddPayment(payment);
            order.Value.Payment.MarkAsPaid();
            order.Value.MarkAsPaid();

            // Act & Assert
            order.Invoking(o => o.Value.CancelOrder())
                .Should().Throw<InvalidOperationException>().WithMessage("Cannot cancel a paid order.");
        }
    }
}
