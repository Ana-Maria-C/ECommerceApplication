using ECommerceApplication.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApplication.Domain.Tests
{
    public class ReceiptTests
    {
        [Fact]
        public void When_CreateReceiptIsCalled_And_OrderIdIsValid_Then_SuccessIsReturned()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();

            // Arrange && Act
            var result = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
            result.Value.OrderId.Should().Be(orderId);
            result.Value.IssueDate.Should().Be(issueDate);
            result.Value.ReceiptId.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void When_CreateReceiptIsCalled_And_OrderIdIsInvalid_Then_FailureIsReturned()
        {
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();

            // Arrange && Act
            var result = Receipt.Create(Guid.Empty, totalAmount, issueDate, appliedDiscounts);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_CreateReceiptIsCalled_And_TotalAmountIsNegative_Then_FailureIsReturned()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = -100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();

            // Arrange && Act
            var result = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_CreateReceiptIsCalled_And_IssueDateIsInTheFuture_Then_FailureIsReturned()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow.AddDays(1);
            List<Discount> appliedDiscounts = new List<Discount>();

            // Arrange && Act
            var result = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_AddDiscountIsCalled_And_DiscountIsNotApplied_Then_DiscountIsAddedSuccessfully()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();
            var receipt = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts).Value;
            var discount = Discount.Create("Test Discount", 10, DateTime.UtcNow.AddDays(30)).Value;

            // Arrange && Act
            receipt.AddDiscount(discount);

            // Assert
            //Assert.True(receipt.AppliedDiscounts.Contains(discount));
            receipt.AppliedDiscounts.Should().Contain(discount);
        }

        [Fact]
        public void When_AddDiscountIsCalled_And_DiscountIsAlreadyApplied_Then_NothingHappens()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();
            var receipt = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts).Value;
            var discount = Discount.Create("Test Discount", 10, DateTime.UtcNow.AddDays(30)).Value;
            receipt.AddDiscount(discount);
            // Arrange && Act
            receipt.AddDiscount(discount);

            // Assert
            //Assert.True(receipt.AppliedDiscounts.Count == 1);
            receipt.AppliedDiscounts.Count.Should().Be(1);
        }

        [Fact]
        public void When_IsDiscountAppliedIsCalled_And_DiscountIsApplied_Then_TrueIsReturned()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();
            var receipt = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts).Value;
            var discount = Discount.Create("Test Discount", 10, DateTime.UtcNow.AddDays(30)).Value;
            receipt.AddDiscount(discount);

            // Arrange && Act
            var result = receipt.IsDiscountApplied(discount);

            // Assert
            //Assert.True(result);
            result.Should().BeTrue();
        }

        [Fact]
        public void When_IsDiscountAppliedIsCalled_And_DiscountIsNotApplied_Then_FalseIsReturned()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();
            var receipt = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts).Value;
            var discount = Discount.Create("Test Discount", 10, DateTime.UtcNow.AddDays(30)).Value;

            // Arrange && Act
            var result = receipt.IsDiscountApplied(discount);

            // Assert
            //Assert.False(result);
            result.Should().BeFalse();
        }

        [Fact]
        public void When_RemoveDiscountIsCalled_And_DiscountIsApplied_Then_DiscountIsRemovedSuccessfully()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();
            var receipt = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts).Value;
            var discount = Discount.Create("Test Discount", 10, DateTime.UtcNow.AddDays(30)).Value;
            receipt.AddDiscount(discount);

            // Arrange && Act
            receipt.RemoveDiscount(discount);

            // Assert
            //Assert.False(receipt.AppliedDiscounts.Contains(discount));
            receipt.AppliedDiscounts.Should().NotContain(discount);
        }

        [Fact]
        public void When_RemoveDiscountIsCalled_And_DiscountIsNotApplied_Then_NothingHappens()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();
            var receipt = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts).Value;
            var discount = Discount.Create("Test Discount", 10, DateTime.UtcNow.AddDays(30)).Value;

            // Arrange && Act
            receipt.RemoveDiscount(discount);

            // Assert
            //Assert.False(receipt.AppliedDiscounts.Contains(discount));
            receipt.AppliedDiscounts.Should().NotContain(discount);
        }

        [Fact]
        public void When_UpdateTotalAmountIsCalled_Then_TotalAmountIsUpdatedSuccessfully()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();
            var receipt = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts).Value;
            decimal newTotalAmount = 200;

            // Arrange && Act
            receipt.UpdateTotalAmount(newTotalAmount);

            // Assert
            //Assert.Equal(newTotalAmount, receipt.TotalAmount);
            receipt.TotalAmount.Should().Be(newTotalAmount);
        }

        [Fact]
        public void When_UpdateTotalAmountIsCalled_WithNegativeAmount_Then_FailureIsReturned()
        {
            Guid orderId = Guid.NewGuid();
            decimal totalAmount = 100;
            DateTime issueDate = DateTime.UtcNow;
            List<Discount> appliedDiscounts = new List<Discount>();
            var receipt = Receipt.Create(orderId, totalAmount, issueDate, appliedDiscounts).Value;
            decimal newTotalAmount = -200;

            // Arrange && Act
            receipt.UpdateTotalAmount(newTotalAmount);

            // Assert
            //Assert.Equal(newTotalAmount, receipt.TotalAmount);
            receipt.TotalAmount.Should().Be(totalAmount);
        }

    }
}
