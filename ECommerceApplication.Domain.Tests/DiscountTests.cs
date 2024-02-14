using ECommerceApplication.Domain.Entities;
using FluentAssertions;

namespace ECommerceApplication.Domain.Tests
{
    public class DiscountTests
    {
        [Fact]
        public void When_CreateDiscountIsCalled_And_DiscountNameIsValid_Then_SuccessIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(30);
            // Arrange && Act
            var result = Discount.Create(code, percentage, expiryDate);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
            result.Value.Code.Should().Be(code);
        }

        [Fact]
        public void When_CreateDiscountIsCalled_And_DiscountNameIsNull_Then_FailureIsReturned()
        {
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(30);
            // Arrange && Act
            var result = Discount.Create(null, percentage, expiryDate);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]

        public void When_CreateDiscountIsCalled_And_PercentageIsNegative_Then_FailureIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = -10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(30);
            // Arrange && Act
            var result = Discount.Create(code, percentage, expiryDate);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_CreateDiscountIsCalled_And_PercentageIsGreaterThan100_Then_FailureIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 101;
            DateTime expiryDate = DateTime.UtcNow.AddDays(30);
            // Arrange && Act
            var result = Discount.Create(code, percentage, expiryDate);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }


        [Fact]
        public void When_CreateDiscountIsCalled_And_PercentageIsPositiveAndSmallerThen100_Then_SuccessIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(30);
            // Arrange && Act
            var result = Discount.Create(code, percentage, expiryDate);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_CreateDiscountIsCalled_And_ExpiryDateIsInThePast_Then_FailureIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(-1);
            // Arrange && Act
            var result = Discount.Create(code, percentage, expiryDate);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_CreateDiscountIsCalled_And_ExpiryDateIsInTheFuture_Then_SuccessIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            // Arrange && Act
            var result = Discount.Create(code, percentage, expiryDate);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
        }


        [Fact]
        public void When_DiscountIsNotExpired_Then_FalseIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            // Arrange && Act
            var result = discount.IsExpired();
            // Assert
            //Assert.True(result.IsSuccess);
            result.Should().BeFalse();
        }

        [Fact]
        public void When_DiscountIsNotExpired_Then_ItIsValidForOrder()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            // Arrange && Act
            var result = discount.IsValidForOrder(DateTime.UtcNow);
            // Assert
            //Assert.True(result.IsSuccess);
            result.Should().BeTrue();
        }

        [Fact]
        public void When_CalculateDiscountAmountIsCalled_Then_CorrectAmountIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            decimal totalAmount = 100;
            // Arrange && Act
            var result = discount.CalculateDiscountAmount(totalAmount);
            // Assert
            //Assert.True(result.IsSuccess);
            result.Should().Be(10);
        }

        [Fact]
        public void When_DiscountIsComparedToNull_Then_FalseIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            // Arrange && Act
            var result = discount.Equals(null);
            // Assert
            //Assert.True(result.IsSuccess);
            result.Should().BeFalse();
        }

        [Fact]
        public void When_DiscountIsComparedToItself_Then_TrueIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            // Arrange && Act
            var result = discount.Equals(discount);
            // Assert
            //Assert.True(result.IsSuccess);
            result.Should().BeTrue();
        }


        [Fact]
        public void When_DiscountIsComparedToAnotherDiscountWithDifferentId_Then_FalseIsReturned()
        {
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount1 = Discount.Create(code, percentage, expiryDate).Value;
            var discount2 = Discount.Create(code, percentage, expiryDate).Value;
            // Arrange && Act
            var result = discount1.Equals(discount2);
            // Assert
            //Assert.True(result.IsSuccess);
            result.Should().BeFalse();
        }

    }
}
