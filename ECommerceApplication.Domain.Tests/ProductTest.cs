using ECommerceApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace ECommerceApplication.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void When_CreateProductIsCalled_WithValidParameters_Then_SuccessIsReturned()
    {
        // Arrange && Act
        var result = Product.Create(Guid.NewGuid(), "Test Product", 10.0m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ProductId.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void When_CreateProductIsCalled_WithInvalidName_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Product.Create(Guid.NewGuid(), "", 10.0m);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Product Name is required.");
    }

    [Fact]
    public void When_CreateProductIsCalled_WithInvalidPrice_Then_FailureIsReturned()
    {
        // Arrange && Act
        var result = Product.Create(Guid.NewGuid(), "Test Product", 0.0m);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("Price must be greater than zero.");
    }

    [Fact]
    public void When_UpdateProductIsCalled_WithValidParameters_Then_ProductIsUpdatedSuccessfully()
    {
        // Arrange
        var product = Product.Create(Guid.NewGuid(), "Initial Product", 20.0m).Value;
        var categoryId = Guid.NewGuid();

        // Act
        product.Update("Updated Product", 30.0m, "Updated Description", "https://example.com/image.jpg", categoryId);

        // Assert
        product.ProductName.Should().Be("Updated Product");
        product.Price.Should().Be(30.0m);
        product.Description.Should().Be("Updated Description");
        product.ImageUrl.Should().Be("https://example.com/image.jpg");
        product.CategoryId.Should().Be(categoryId);
    }

    [Fact]
    public void When_AttachDescriptionIsCalled_WithValidDescription_Then_DescriptionIsAttached()
    {
        // Arrange
        var product = Product.Create(Guid.NewGuid(), "Test Product", 10.0m).Value;

        // Act
        product.AttachDescription("Test Description");

        // Assert
        product.Description.Should().Be("Test Description");
    }

    [Fact]
    public void When_AttachDescriptionIsCalled_WithEmptyDescription_Then_DescriptionIsNotAttached()
    {
        // Arrange
        var product = Product.Create(Guid.NewGuid(), "Test Product", 10.0m).Value;

        // Act
        product.AttachDescription("");

        // Assert
        product.Description.Should().BeNull();
    }

    [Fact]
    public void When_AttachImageUrlIsCalled_WithValidImageUrl_Then_ImageUrlIsAttached()
    {
        // Arrange
        var product = Product.Create(Guid.NewGuid(), "Test Product", 10.0m).Value;

        // Act
        product.AttachImageUrl("https://example.com/image.jpg");

        // Assert
        product.ImageUrl.Should().Be("https://example.com/image.jpg");
    }

    [Fact]
    public void When_AttachImageUrlIsCalled_WithEmptyImageUrl_Then_ImageUrlIsNotAttached()
    {
        // Arrange
        var product = Product.Create(Guid.NewGuid(), "Test Product", 10.0m).Value;

        // Act
        product.AttachImageUrl("");

        // Assert
        product.ImageUrl.Should().BeNull();
    }

    [Fact]
    public void When_AttachCategoryIsCalled_WithValidCategory_Then_CategoryIsAttached()
    {
        // Arrange
        var product = Product.Create(Guid.NewGuid(), "Test Product", 10.0m).Value;
        var category = Category.Create("Test Category");

        // Act
        product.AttachCategory(category.Value);

        // Assert
        product.CategoryId.Should().Be(category.Value.CategoryId);
        product.Category.Should().Be(category.Value);
    }

    [Fact]
    public void When_AddReviewIsCalled_Then_ReviewIsAddedToProduct()
    {
        // Arrange
        var product = Product.Create(Guid.NewGuid(), "Test Product", 10.0m).Value;
        var review = Review.Create(product.ProductId, Guid.NewGuid(), "Good product", 5);

        // Act
        product.AddReview(review.Value);

        // Assert
        product.Reviews.Should().Contain(review.Value);
    }
}