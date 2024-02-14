// Import necessary namespaces

using ECommerceApplication.Domain.Entities;
using FluentAssertions;

namespace ECommerceApplication.Domain.Tests
{
    public class ReviewTests
    {
        [Fact]
        public void When_CreateReviewIsCalled_WithValidParameters_Then_SuccessIsReturned()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            var result = Review.Create(productId, userId, "Good product", 5.0m);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.ReviewId.Should().NotBe(Guid.Empty);
            result.Value.ProductId.Should().Be(productId);
            result.Value.UserId.Should().Be(userId);
        }

        [Fact]
        public void When_CreateReviewIsCalled_WithInvalidProductId_Then_FailureIsReturned()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var result = Review.Create(Guid.Empty, userId, "Good product", 5.0m);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Product id is required.");
        }

        [Fact]
        public void When_CreateReviewIsCalled_WithInvalidUserId_Then_FailureIsReturned()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            var result = Review.Create(productId, Guid.Empty, "Good product", 5.0m);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("User id is required.");
        }

        [Fact]
        public void When_CreateReviewIsCalled_WithInvalidReviewText_Then_FailureIsReturned()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            var result = Review.Create(productId, userId, "", 5.0m);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Review text is required.");
        }

        [Fact]
        public void When_CreateReviewIsCalled_WithInvalidRating_Then_FailureIsReturned()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            var result = Review.Create(productId, userId, "Good product", 0.0m);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Rating is required.");
        }

        [Fact]
        public void When_UpdateReviewIsCalled_WithValidParameters_Then_ReviewIsUpdatedSuccessfully()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var review = Review.Create(productId, userId, "Initial review", 4.0m).Value;

            // Act
            review.UpdateReview("Updated review", 5.0m);

            // Assert
            review.ReviewText.Should().Be("Updated review");
            review.Rating.Should().Be(5.0m);
        }
    }
}
