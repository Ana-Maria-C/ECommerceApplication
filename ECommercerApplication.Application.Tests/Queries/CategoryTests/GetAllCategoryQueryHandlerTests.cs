using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using ECommerceApplication.Application.Features.Categories.Queries.GetAllCategory;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ECommerceApplication.Domain.Common;


namespace ECommercerApplication.Application.Tests.Queries.CategoryTests
{
    public class GetAllCategoryQueryHandlerTests
    {
        private readonly ICategoryRepository _repository;
        private readonly GetAllCategoryQueryHandler _handler;

        public GetAllCategoryQueryHandlerTests()
        {
            _repository = Substitute.For<ICategoryRepository>();
            _handler = new GetAllCategoryQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
        {
            Category.Create("Category 1").Value,
            Category.Create("Category 2").Value
            // Add more categories as needed
        };
            var result = Result<IReadOnlyList<Category>>.Success(categories);
            _repository.GetAllAsync().Returns(Task
                .FromResult<Result<IReadOnlyList<Category>>>(result));

            // Act
            var response = await _handler.Handle(new GetAllCategoryQuery(), new CancellationToken());

            // Assert
            Assert.NotNull(response.Categories);
            Assert.Equal(categories.Count, response.Categories.Count);
            Assert.All(response.Categories, categoryDto =>
                Assert.Contains(categories, c => c.CategoryId == categoryDto.CategoryId && c.CategoryName == categoryDto.CategoryName));
        }

        // Additional test cases as needed...
    }
}
