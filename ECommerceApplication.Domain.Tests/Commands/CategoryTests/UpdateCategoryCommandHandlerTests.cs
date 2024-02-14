using ECommerceApplication.Domain.Common;

namespace ECommercerApplication.Application.Tests.Commands.CategoryTests
{
    using Xunit;
    using NSubstitute;
    using ECommerceApplication.Application.Features.Categories.Commands.UpdateCategory;
    using ECommerceApplication.Application.Persistence;
    using ECommerceApplication.Domain.Entities;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    public class UpdateCategoryCommandHandlerTests
    {
        private readonly ICategoryRepository _repository;
        private readonly UpdateCategoryCommandHandler _handler;

        public UpdateCategoryCommandHandlerTests()
        {
            _repository = Substitute.For<ICategoryRepository>();
            _handler = new UpdateCategoryCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_InvalidInput_ReturnsFailure()
        {
            // Arrange
            var invalidCommand = new UpdateCategoryCommand { CategoryId = Guid.NewGuid(), CategoryName = "" }; // Invalid due to empty name

            // Act
            var response = await _handler.Handle(invalidCommand, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.NotEmpty(response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_CategoryNotFound_ReturnsFailure()
        {
            // Arrange
            var command = new UpdateCategoryCommand { CategoryId = new Guid(), CategoryName = "nu avem" };
            _repository.FindByIdAsync(Arg.Any<Guid>())
                .Returns(Task
                .FromResult<Result<Category>>(Result<Category>.Failure($"Entity with id not found")));

            // Act
            var response = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.True(!response.Success);
        }

        [Fact]
        public async Task Handle_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var mockCategory = Category.Create("ExistingName").Value; // Using the static Create method to instantiate
            var validCommand = new UpdateCategoryCommand { CategoryId = mockCategory.CategoryId, CategoryName = "Electronics" };
            _repository.FindByIdAsync(mockCategory.CategoryId)
                .Returns(Task
                .FromResult<Result<Category>>(Result<Category>.Success(Category.Create("Electronics").Value)));
            _repository.UpdateAsync(Arg.Any<Category>()).Returns(Task
                .FromResult<Result<Category>>(Result<Category>.Success(Category.Create("Electronics").Value)));

            // Act
            var response = await _handler.Handle(validCommand, new CancellationToken());

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response.Category);
            Assert.Equal("Electronics", response.Category.CategoryName);
        }

        [Fact]
        public async Task Handle_UpdateFailure_ReturnsFailure()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var command = new UpdateCategoryCommand { CategoryId = categoryId, CategoryName = "" }; // Invalid update due to empty name
            var mockCategory = Category.Create("ExistingName").Value;
            _repository.FindByIdAsync(categoryId)
                .Returns(Task
                .FromResult<Result<Category>>(Result<Category>.Success(Category.Create("Electronics").Value))); ;

            // Act
            var response = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.Contains("Category Name is required.", response.ValidationsErrors);
        }
    }

}
