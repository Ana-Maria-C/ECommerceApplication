using Xunit;
using NSubstitute;
using ECommerceApplication.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Application.Features.Categories.Commands.CreateCategory;

namespace ECommercerApplication.Application.Tests.Commands.CategoryTests
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly ICategoryRepository _repository;
        private readonly CreateCategoryCommandHandler _handler;

        public CreateCategoryCommandHandlerTests()
        {
            _repository = Substitute.For<ICategoryRepository>();
            _handler = new CreateCategoryCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_InvalidInput_ReturnsFailure()
        {
            var invalidCommand = new CreateCategoryCommand { CategoryName = "" };
            var response = await _handler.Handle(invalidCommand, new CancellationToken());
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.NotEmpty(response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var validCommand = new CreateCategoryCommand { CategoryName = "Electronics" };
            //_repository.AddAsync(Arg.Any<Category>()).Returns(Task.CompletedTask);

            // Act
            var response = await _handler.Handle(validCommand, new CancellationToken());

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response.Data);
            Assert.Equal("Electronics", response.Data.CategoryName);
        }



        [Fact]
        public async Task Handle_CategoryCreationFailure_ReturnsFailure()
        {
            string invalidCategoryName = "";
            var commandWithCreationIssue = new CreateCategoryCommand { CategoryName = invalidCategoryName };
            var response = await _handler.Handle(commandWithCreationIssue, new CancellationToken());
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.NotEmpty(response.ValidationsErrors);
        }

        [Fact]
        public async Task Handle_RepositoryAddFailure_ReturnsFailure()
        {
            var validCommand = new CreateCategoryCommand { CategoryName = "Electronics" };
            _repository.When(r => r.AddAsync(Arg.Any<Category>())).Do(x => { throw new Exception("Database error"); });
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(validCommand, new CancellationToken()));
            Assert.Equal("Database error", exception.Message);
        }
    }
}