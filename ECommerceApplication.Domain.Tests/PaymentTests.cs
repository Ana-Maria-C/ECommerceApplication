using ECommerceApplication.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Domain.Tests
{
    public class PaymentTests
    {
        [Fact]
        public void When_CreatePaymentIsCalled_And_PaymentInfoAreValid_Then_SuccessIsReturned()
        {
            Guid userId = Guid.NewGuid();
            decimal paymentAmount = 10;
            DateTime paymentDate = DateTime.UtcNow.AddDays(30);
            string paymentMethod ="Credit Card";
            // Arrange && Act
            var result = Payment.Create(userId, paymentAmount, paymentDate, paymentMethod);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
            result.Value.PaymentId.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void When_CreatePaymentIsCalled_And_UserIdIsDefault_Then_FailureIsReturned()
        {
            Guid userId = default;
            decimal paymentAmount = 10;
            DateTime paymentDate = DateTime.UtcNow.AddDays(30);
            string paymentMethod = "Credit Card";
            // Arrange && Act
            var result = Payment.Create(userId, paymentAmount, paymentDate, paymentMethod);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();

        }

        [Fact]
        public void When_CreatePaymentIsCalled_And_PaymentAmountIsSmallerThanZero_Then_FailureIsReturned()
        {
            Guid userId = Guid.NewGuid();
            decimal paymentAmount = -19;
            DateTime paymentDate = DateTime.UtcNow.AddDays(30);
            string paymentMethod = "Credit Card";
            // Arrange && Act
            var result = Payment.Create(userId, paymentAmount, paymentDate, paymentMethod);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_CreatePaymentIsCalled_And_PaymentAmountIsZero_Then_FailureIsReturned()
        {
            Guid userId = Guid.NewGuid();
            decimal paymentAmount = 0;
            DateTime paymentDate = DateTime.UtcNow.AddDays(30);
            string paymentMethod = "Credit Card";
            // Arrange && Act
            var result = Payment.Create(userId, paymentAmount, paymentDate, paymentMethod);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_CreatePaymentIsCalled_And_PaymentDateIsDefault_Then_FailureIsReturned()
        {
            Guid userId = Guid.NewGuid();
            decimal paymentAmount = 10;
            DateTime paymentDate = default;
            string paymentMethod = "Credit Card";
            // Arrange && Act
            var result = Payment.Create(userId, paymentAmount, paymentDate, paymentMethod);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_CreatePaymentIsCalled_And_PaymentMethodIsInvalid_Then_FailureIsReturned()
        {
            Guid userId = Guid.NewGuid();
            decimal paymentAmount = 10;
            DateTime paymentDate = DateTime.UtcNow.AddDays(30);
            string paymentMethod = "";
            // Arrange && Act
            var result = Payment.Create(userId, paymentAmount, paymentDate, paymentMethod);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_IsPaymentValidIsCalled_And_PaymentStatusIsPaid_Then_TrueIsReturned()
        {
            Guid userId = Guid.NewGuid();
            decimal paymentAmount = 10;
            DateTime paymentDate = DateTime.UtcNow.AddDays(30);
            string paymentMethod = "Credit Card";
            // Arrange && Act
            var result = Payment.Create(userId, paymentAmount, paymentDate, paymentMethod);
            result.Value.AttachPaymentStatus("Paid");
            result.Value.IsPaymentValid();
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
        }
 

        [Fact]
        public void When_MarkAsPaidIsCalled_And_PaymentStatusIsPending_Then_PaymentStatusIsUpdated()
        {
            Guid userId = Guid.NewGuid();
            decimal paymentAmount = 10;
            DateTime paymentDate = DateTime.UtcNow.AddDays(30);
            string paymentMethod = "Credit Card";
            // Arrange && Act
            var result = Payment.Create(userId, paymentAmount, paymentDate, paymentMethod);
            result.Value.MarkAsPaid();
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_MarkAsPaidIsCalled_And_PaymentStatusIsAlreadyPaid_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            payment.MarkAsPaid();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => payment.MarkAsPaid());
        }

        [Fact]
        public void When_UpdatePaymentMethodIsCalled_And_NewPaymentMethodIsValid_Then_PaymentMethodIsUpdated()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            string newPaymentMethod = "DebitCard";

            // Act
            payment.UpdatePaymentMethod(newPaymentMethod);

            // Assert
            payment.PaymentMethod.Should().Be(newPaymentMethod);
        }

        [Fact]
        public void When_UpdatePaymentMethodIsCalled_And_NewPaymentMethodIsInvalid_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            string newPaymentMethod = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => payment.UpdatePaymentMethod(newPaymentMethod));
        }

        [Fact]
        public void When_UpdatePaymentAmountIsCalled_And_NewAmountIsValid_Then_PaymentAmountIsUpdated()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            decimal newAmount = 150.00m;

            // Act
            payment.UpdatePaymentAmount(newAmount);

            // Assert
            payment.Amount.Should().Be(newAmount);
        }

        [Fact]
        public void When_UpdatePaymentAmountIsCalled_And_NewAmountIsInvalid_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            decimal newAmount = default;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => payment.UpdatePaymentAmount(newAmount));
        }

        [Fact]
        public void When_AttachPaymentStatusIsCalled_Then_PaymentStatusIsAttached()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            string paymentStatus = "Pending";

            // Act
            payment.AttachPaymentStatus(paymentStatus);

            // Assert
            payment.PaymentStatus.Should().Be(paymentStatus);
        }

        [Fact]
        public void When_AttachPaymentDateIsCalled_Then_PaymentDateIsAttached()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            DateTime newPaymentDate = DateTime.UtcNow.AddDays(2);

            // Act
            payment.AttachPaymentDate(newPaymentDate);

            // Assert
            payment.PaymentDate.Should().Be(newPaymentDate);
        }

        [Fact]
        public void When_AttachPaymentMethodIsCalled_Then_PaymentMethodIsAttached()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            string paymentMethod = "PayPal";

            // Act
            payment.AttachPaymentMethod(paymentMethod);

            // Assert
            payment.PaymentMethod.Should().Be(paymentMethod);
        }

        [Fact]
        public void When_AttachCurrencyIsCalled_Then_CurrencyIsAttached()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            string currency = "USD";

            // Act
            payment.AttachCurrency(currency);

            // Assert
            payment.Currency.Should().Be(currency);
        }

        [Fact]
        public void When_AttachAmountIsCalled_Then_AmountIsAttached()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            decimal amount = 200.00m;

            // Act
            payment.AttachAmount(amount);

            // Assert
            payment.Amount.Should().Be(amount);
        }

        [Fact]
        public void When_AttachOrderIdIsCalled_Then_OrderIdIsAttached()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            Guid orderId = Guid.NewGuid();

            // Act
            payment.AttachOrderId(orderId);

            // Assert
            payment.OrderId.Should().Be(orderId);
        }

        [Fact]
        public void When_UpdateIsCalled_WithValidParameters_Then_PaymentIsUpdated()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            Guid newOrderId = Guid.NewGuid();
            decimal newAmount = 200.00m;
            DateTime newPaymentDate = DateTime.UtcNow.AddDays(5);
            string newPaymentMethod = "DebitCard";
            string newPaymentStatus = "Paid";
            string newCurrency = "EUR";

            // Act
            payment.Update(newOrderId, newAmount, newPaymentDate, newPaymentMethod, newPaymentStatus, newCurrency);

            // Assert
            payment.OrderId.Should().Be(newOrderId);
            payment.Amount.Should().Be(newAmount);
            payment.PaymentDate.Should().Be(newPaymentDate);
            payment.PaymentMethod.Should().Be(newPaymentMethod);
            payment.PaymentStatus.Should().Be(newPaymentStatus);
            payment.Currency.Should().Be(newCurrency);
        }

        [Fact]
        public void When_UpdateIsCalled_WithInvalidOrderId_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            Guid newOrderId = default;
            decimal newAmount = 200.00m;
            DateTime newPaymentDate = DateTime.UtcNow.AddDays(5);
            string newPaymentMethod = "DebitCard";
            string newPaymentStatus = "Paid";
            string newCurrency = "EUR";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => payment.Update(newOrderId, newAmount, newPaymentDate, newPaymentMethod, newPaymentStatus, newCurrency));
        }

        [Fact]
        public void When_UpdateIsCalled_WithInvalidAmount_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            Guid newOrderId = Guid.NewGuid();
            decimal newAmount = default;
            DateTime newPaymentDate = DateTime.UtcNow.AddDays(5);
            string newPaymentMethod = "DebitCard";
            string newPaymentStatus = "Paid";
            string newCurrency = "EUR";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => payment.Update(newOrderId, newAmount, newPaymentDate, newPaymentMethod, newPaymentStatus, newCurrency));
        }

        [Fact]
        public void When_UpdateIsCalled_WithInvalidPaymentDate_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            Guid newOrderId = Guid.NewGuid();
            decimal newAmount = 200.00m;
            DateTime newPaymentDate = default;
            string newPaymentMethod = "DebitCard";
            string newPaymentStatus = "Paid";
            string newCurrency = "EUR";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => payment.Update(newOrderId, newAmount, newPaymentDate, newPaymentMethod, newPaymentStatus, newCurrency));
        }

        [Fact]
        public void When_UpdateIsCalled_WithInvalidPaymentMethod_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            Guid newOrderId = Guid.NewGuid();
            decimal newAmount = 200.00m;
            DateTime newPaymentDate = DateTime.UtcNow.AddDays(5);
            string newPaymentMethod = "";
            string newPaymentStatus = "Paid";
            string newCurrency = "EUR";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => payment.Update(newOrderId, newAmount, newPaymentDate, newPaymentMethod, newPaymentStatus, newCurrency));
        }

        [Fact]
        public void When_UpdateIsCalled_WithInvalidPaymentStatus_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            Guid newOrderId = Guid.NewGuid();
            decimal newAmount = 200.00m;
            DateTime newPaymentDate = DateTime.UtcNow.AddDays(5);
            string newPaymentMethod = "DebitCard";
            string newPaymentStatus = "";
            string newCurrency = "EUR";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => payment.Update(newOrderId, newAmount, newPaymentDate, newPaymentMethod, newPaymentStatus, newCurrency));
        }

        [Fact]
        public void When_UpdateIsCalled_WithInvalidCurrency_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;
            Guid newOrderId = Guid.NewGuid();
            decimal newAmount = 200.00m;
            DateTime newPaymentDate = DateTime.UtcNow.AddDays(5);
            string newPaymentMethod = "DebitCard";
            string newPaymentStatus = "Paid";
            string newCurrency = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => payment.Update(newOrderId, newAmount, newPaymentDate, newPaymentMethod, newPaymentStatus, newCurrency));
        }

        [Fact]
        public void When_MarkAsFailedIsCalled_And_PaymentStatusIsPending_Then_PaymentStatusIsUpdated()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;

            // Act
            payment.MarkAsFailed();

            // Assert
            payment.PaymentStatus.Should().Be("Failed");
        }

        [Fact]
        public void When_MarkAsFailedIsCalled_And_PaymentStatusIsPaid_Then_ExceptionIsThrown()
        {
            // Arrange
            var payment = Payment.Create(Guid.NewGuid(), 100.00m, DateTime.UtcNow, "CreditCard").Value;

            // Act
            payment.MarkAsPaid();

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => payment.MarkAsFailed());
            exception.Message.Should().Be("Payment has already been processed or is invalid.");
        }

    }
}
