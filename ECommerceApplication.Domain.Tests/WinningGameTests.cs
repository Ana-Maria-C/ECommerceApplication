using ECommerceApplication.Domain.Entities;
using FluentAssertions;

namespace ECommerceApplication.Domain.Tests
{
    public class WinningGameTests
    {
        [Fact]
        public void When_CreateWinningGamesIsCalled_WithValidParameters_Then_SuccessIsReturned()
        {
            Guid userId = Guid.NewGuid();
            int price = 10;
            DateTime timestamp = DateTime.UtcNow.AddDays(2);

            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);

            var discount = Discount.Create(code, percentage, expiryDate).Value;
            List<Discount> discounts = [discount];
            int size = 10;
            // Arrange && Act
            var result = WinningGame.Create(userId, price, timestamp, discounts,size);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
            result.Value.WinningGameId.Should().NotBe(Guid.Empty);
            result.Value.UserId.Should().Be(userId);
            result.Value.Timestamp.Should().Be(timestamp);

        }

        [Fact]
        public void When_CreateWinningGamesIsCalled_And_TimeStampIsInThePast_Then_FailureIsReturned()
        {
            int price = 10;
            DateTime timestamp = DateTime.UtcNow.AddDays(-1);
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            List<Discount> discounts = new List<Discount>();
            discounts.Add(discount);
            int size = 10;
            // Arrange && Act
            var result = WinningGame.Create(Guid.NewGuid(), price, timestamp, discounts,size);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Timestamp is required");
        }

        [Fact]
        public void When_CreateWinningGamesIsCalled_And_UserIdIsEmpty_Then_FailureIsReturned()
        {
            int price = 10;
            DateTime timestamp = DateTime.UtcNow;
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            List<Discount> discounts = new List<Discount>();
            discounts.Add(discount);
            int size = 10;
            // Arrange && Act
            var result = WinningGame.Create(Guid.Empty, price, timestamp, discounts, size);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("User id is required.");
        }

        [Fact]
        public void When_CreateWinningGamesIsCalled_And_PriceIsNegative_Then_FailureIsReturned()
        {
            Guid userId = Guid.NewGuid();
            int price = -10;
            DateTime timestamp = DateTime.UtcNow.AddDays(2);
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            List<Discount> discounts = new List<Discount>();
            discounts.Add(discount);
            int size = 10;
            // Arrange && Act
            var result = WinningGame.Create(userId, price, timestamp, discounts,size);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_CreateWinningGamesIsCalled_And_SizeIsNegative_Then_FailureIsReturned()
        {
            Guid userId = Guid.NewGuid();
            int price = 10;
            DateTime timestamp = DateTime.UtcNow.AddDays(2);
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            List<Discount> discounts = new List<Discount>();
            discounts.Add(discount);
            int size = -10;
            // Arrange && Act
            var result = WinningGame.Create(userId, price, timestamp, discounts,size);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_AddDiscountIsCalled_And_DiscountIsValid_Then_SuccessIsReturned()
        {
            Guid userId = Guid.NewGuid();
            int price = 10;
            DateTime timestamp = DateTime.UtcNow.AddDays(2);

            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;

            string code2 = "Test2 Discount";
            decimal percentage2 = 20;
            DateTime expiryDate2 = DateTime.UtcNow.AddDays(1);
            var discount2 = Discount.Create(code, percentage, expiryDate).Value;

            List<Discount> discounts = new List<Discount>();
            int size = 10;
            var result = WinningGame.Create(userId, price, timestamp, discounts, size);

            List<Discount> newDiscount = new List<Discount>();
            newDiscount.Add(discount);
            newDiscount.Add(discount2);

            // Arrange && Act
            result.Value.AddDiscount(newDiscount);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_RemoveDiscountIsCalled_WithDiscountAlreadyIn_Then_DiscountIsRemovedFromWinningGame()
        {
            Guid userId = Guid.NewGuid();
            int price = 10;
            DateTime timestamp = DateTime.UtcNow.AddDays(2);
            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            List<Discount> discounts = new List<Discount>();
            discounts.Add(discount);
            int size = 10;
            var result = WinningGame.Create(userId, price, timestamp, discounts, size);

            // Arrange && Act
            result.Value.RemoveDiscount(discount);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();  
        }

        [Fact]
        public void When_SpinWheelIsCalled_And_RewardValueIsBiggerThanPrice_Then_SuccessIsReturned()
        {
            Guid userId = Guid.NewGuid();
            int price = 10;
            DateTime timestamp = DateTime.UtcNow.AddDays(2);

            string code = "Test Discount";
            decimal percentage = 10;
            DateTime expiryDate = DateTime.UtcNow.AddDays(1);
            var discount = Discount.Create(code, percentage, expiryDate).Value;
            List<Discount> discounts = new List<Discount>();
            discounts.Add(discount);
            int size = 10;
            var result = WinningGame.Create(userId, price, timestamp, discounts, size);

            decimal rewardValue = 10;
            DateTime expiryDateReward = DateTime.UtcNow.AddDays(30);
            // Arrange && Act
            var reward = Reward.Create(userId, rewardValue, expiryDateReward).Value;

            // Arrange && Act
            result.Value.SpinWheel(reward);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
        }

    }
}
