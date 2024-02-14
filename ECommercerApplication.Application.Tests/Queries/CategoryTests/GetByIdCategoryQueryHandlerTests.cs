using ECommerceApplication.Application.Features.Categories.Queries.GetAllCategory;
using ECommerceApplication.Application.Features.Categories.Queries.GetByIdCategory;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercerApplication.Application.Tests.Queries.CategoryTests
{
    using Xunit;
    using NSubstitute;
    using ECommerceApplication.Application.Features.Categories.Queries.GetByIdCategory;
    using ECommerceApplication.Application.Persistence;
    using ECommerceApplication.Domain.Entities;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetByIdCategoryQueryHandlerTests
    {
        private readonly ICategoryRepository _repository;
        private readonly GetByIdCategoryQueryHandler _handler;

        public GetByIdCategoryQueryHandlerTests()
        {
            _repository = Substitute.For<ICategoryRepository>();
            _handler = new GetByIdCategoryQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_CategoryFound_ReturnsCategoryDto()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var mockCategory = Category.Create("Test Category").Value;
            _repository.FindByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(Result<Category>.Success(mockCategory)));

            var query = new GetByIdCategoryQuery { CategoryId = categoryId };

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockCategory.CategoryId, result.CategoryId);
            Assert.Equal("Test Category", result.CategoryName);
        }

        [Fact]
        public async Task Handle_CategoryNotFound_ReturnsEmptyDto()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _repository.FindByIdAsync(categoryId).Returns(Task.FromResult(Result<Category>.Failure("Not found")));

            var query = new GetByIdCategoryQuery { CategoryId = categoryId };

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.CategoryId);
            Assert.Null(result.CategoryName); // or Assert.Empty, based on CategoryDto implementation
        }

        // Additional test cases if any specific edge cases exist...
    }

}
