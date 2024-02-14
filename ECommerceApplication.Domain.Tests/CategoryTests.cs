using ECommerceApplication.Domain.Entities;
using FluentAssertions;

namespace ECommerceApplication.Domain.Tests
{
    public class CategoryTests
    {
        [Fact]
        public void When_CreateCategoryIsCalled_And_CategoryNameIsValid_Then_SuccessIsReturned()
        {
            // Arrange && Act
            var result = Category.Create("Test Category");
            var createdDate = DateTime.Now;
            var lastModifiedDate = DateTime.Now;
            var createdBy = "ADMIN";
            var modifiedBy = "ADMIN";
            result.Value.CreatedDate = createdDate;
            result.Value.LastModifiedDate = lastModifiedDate;
            result.Value.CreatedBy = "ADMIN";
            result.Value.LastModifiedBy = "ADMIN";
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeTrue();
            result.Value.CreatedDate.Should().Be(createdDate);
            result.Value.LastModifiedDate.Should().Be(lastModifiedDate);
            result.Value.CreatedBy.Should().Be(createdBy);
            result.Value.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void When_CreateCategoryIsCalled_And_CategoryNameIsNull_Then_FailureIsReturned()
        {
            // Arrange && Act
            var result = Category.Create(null);
            // Assert
            //Assert.True(result.IsSuccess);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void When_UpdateCategoryIsCalled_And_CategoryNameIsValid_Then_SuccessIsReturned()
        {
            // Arrange
            var category = Category.Create("Initial Category"); // Create an initial category

            // Act
            var result = category.Value.Update("Updated Category");

            // Assert
            result.IsSuccess.Should().BeTrue();
            category.Value.CategoryName.Should().Be("Updated Category");
        }

        [Fact]
        public void When_UpdateCategoryIsCalled_And_CategoryNameIsNull_Then_FailureIsReturned()
        {
            // Arrange
            var category = Category.Create("Initial Category"); // Create an initial category

            // Act
            var result = category.Value.Update(null);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

    }
}