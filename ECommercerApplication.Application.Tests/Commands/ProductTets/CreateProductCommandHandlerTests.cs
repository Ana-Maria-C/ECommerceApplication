using ECommerceApplication.Application.Contracts;
using ECommerceApplication.Application.Features.Categories.Commands.CreateCategory;
using ECommerceApplication.Application.Features.Products.Commands.CreateProduct;
using ECommerceApplication.Application.Persistence;
using ECommerceApplication.Domain.Common;
using ECommerceApplication.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
uint Moq;

namespace ECommercerApplication.Application.Tests.Commands.ProductTets
{
    public class CreateProductCommandHandlerTests
    {
        private readonly CreateProductCommandHandler _handler;
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IEmailService emailService;
        private readonly ILogger<CreateProductCommandHandler> logger;

        public CreateProductCommandHandlerTests()
        {
            productRepository = Substitute.For<IProductRepository>();
            _handler = new CreateProductCommandHandler(productRepository, categoryRepository, emailService, logger);
            categoryRepository = Substitute.For<ICategoryRepository>();
            emailService = Substitute.For<IEmailService>();
            logger = Substitute.For<ILogger<CreateProductCommandHandler>>();

        }

        [Fact]
        public async Task Handle_InvalidInput_ReturnsFailure()
        {
            // Arrange
            var invalidCommand = new CreateProductCommand { ProductName = "", Price = -3 }; // Invalid due to empty name

            // Act
            var response = await _handler.Handle(invalidCommand, new CancellationToken());

            // Assert
            Assert.False(response.Success);
            Assert.NotNull(response.ValidationsErrors);
            Assert.NotEmpty(response.ValidationsErrors);
        }

        /*[Fact]
        public async Task Handle_ValidInput_ReturnsSuccess()
        {
            var categoryHandler = new CreateCategoryCommandHandler(categoryRepository);

            var createCategoryCommand = new CreateCategoryCommand { CategoryName = "Electronics" };
            var categoryResponse = await categoryHandler.Handle(createCategoryCommand, new CancellationToken());

            *//*Assert.True(categoryResponse.Success);
            Assert.NotNull(categoryResponse.Category);
            Assert.Equal("Electronics", categoryResponse.Category.CategoryName);*//*

            // Use the created category's ID in the product creation

            var validCommand = new CreateProductCommand
            {
                ProductName = "Valid Product",
                Price = 10.0m, 
                CategoryId = categoryResponse.Category.CategoryId, 
                Description = "Valid Description",
                ImageUrl = "image.url"
            };

            // Act
            var response = await _handler.Handle(validCommand, new CancellationToken());

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response.Product);
            Assert.Equal(validCommand.ProductName, response.Product.ProductName);
            Assert.Equal(validCommand.Price, response.Product.Price);
            Assert.Equal(validCommand.Description, response.Product.Description);
            Assert.Equal(validCommand.ImageUrl, response.Product.ImageUrl);
            Assert.Equal(categoryResponse.Category.CategoryId, response.Product.Category.CategoryId);

        
    }*/

    }
}
