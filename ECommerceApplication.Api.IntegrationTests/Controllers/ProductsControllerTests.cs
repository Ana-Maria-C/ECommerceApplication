using System.ComponentModel;
using FluentAssertions;
using ECommerceApplication.API.IntegrationTests.Base;
using ECommerceApplication.Application.Features.Products.Queries;
using ECommerceApplication.Application.Features.Products.Commands.CreateProduct;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using ECommerceApplication.Application.Features;
using ECommerceApplication.Application.Features.Categories.Commands.CreateCategory;
using ECommerceApplication.Api.IntegrationTests.Controllers;
using Xunit.Abstractions;
using System.Net;

namespace ECommerceApplication.API.IntegrationTests.Controllers
{

    public class ProductsControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/products";

        private readonly ITestOutputHelper _output;

        public ProductsControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task When_GetAllProductsQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<List<ProductDto>>>(responseString);
            // Assert
            result?.Products?.Count.Should().Be(2);

            if (result != null && result.Products != null)
            {
                // Check the count of products
                int productCount = result.Products.Count;
                _output.WriteLine($"Number of products: {productCount}");

                // Accessing the first product's name
                if (productCount > 0)
                {
                    string firstProductName = result.Products[0]?.ProductName;
                    _output.WriteLine($"First product name: {firstProductName}");
                }
            }
        }

        [Fact]
        public async Task
            When_PostProductsCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange and Act

            var responseCategory = await Client.GetAsync("/api/v1/categories");
            responseCategory.EnsureSuccessStatusCode();
            var responseCategoryString = await responseCategory.Content.ReadAsStringAsync();
            var resultCategory = JsonConvert.DeserializeObject<List<CategoryDto>>(responseCategoryString);

            var product = new CreateProductCommand
            {
                ProductName = "ProductName",
                Price = 10,
                Description = "Description",
                ImageUrl = "ImageUrl",
                CategoryId = resultCategory[1].CategoryId
            };

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, product);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<ProductDto>>(responseString);
            result?.Should().NotBeNull();
        }

        [Fact]
        public async Task When_PostProductCommandHandlerIsCalledWithInvalidProductName_Then_BadRequestShouldBeReturned()
        {
            var product = new CreateProductCommand
            {
                ProductName = "", // Invalid product name
                Price = 0,
                Description = "k",
                ImageUrl = "",
                CategoryId = Guid.Empty
            };
            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, product);

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            _output.WriteLine(responseString);
            var result = JsonConvert.DeserializeObject<ProductApiResponse<ProductDto>>(responseString);
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task When_GetByIdProductQueryHandlerIsCalled_WithInvalidProductId_Then_ReturnNotFound()
        {
            // Arrange
            var invalidProductId = Guid.NewGuid();

            // Act
            var response = await Client.GetAsync($"{RequestUri}/{invalidProductId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
