using FluentAssertions;
using ECommerceApplication.API.IntegrationTests.Base;
using ECommerceApplication.Application.Features;
using ECommerceApplication.Application.Features.Categories.Commands.CreateCategory;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Net;
using ECommerceApplication.Api.IntegrationTests.Controllers;
using Xunit.Abstractions;
using ECommerceApplication.Application.Features.Categories.Commands.UpdateCategory;

namespace ECommerceApplication.API.IntegrationTests.Controllers
{

    public class CategoriesControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/categories";

        private readonly ITestOutputHelper _output;

        public CategoriesControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task When_GetAllCategoriesQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<CategoryDto>>(responseString);
            // Assert
            result?.Count().Should().Be(4);
        }

        [Fact]
        public async Task When_PostCategoryCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {

            var category = new CreateCategoryCommand
            {
                CategoryName = "Test Category"
            };
            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, category);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<App.Services.Responses.ApiResponse<CategoryDto>>(responseString);
            result?.Should().NotBeNull();
            result?.Data?.CategoryName.Should().Be(category.CategoryName);
        }

        [Fact]
        public async Task When_PostCategoryCommandHandlerIsCalledWithInvalidParameters_Then_BadRequestShouldBeReturned()
        {
            var category = new CreateCategoryCommand
            {
                CategoryName = "" // Invalid category name
            };
            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, category);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task When_DeleteCategoryCommandHandlerIsCalled_Then_CategoryShouldBeDeleted()
        {
            var category = new CreateCategoryCommand
            {
                CategoryName = "Test Category"
            };

            var category2 = new CreateCategoryCommand
            {
                CategoryName = "Test Category2"
            };
            // Act
            var postResponse = await Client.PostAsJsonAsync(RequestUri, category);
            var postResponse2 = await Client.PostAsJsonAsync(RequestUri, category2);
            var postResponseString = await postResponse.Content.ReadAsStringAsync();
            var categoryDto = JsonConvert.DeserializeObject<CategoryApiResponse<CategoryDto>>(postResponseString);

            var response = await Client.GetAsync(RequestUri);
            var responseString = await response.Content.ReadAsStringAsync();
            _output.WriteLine(responseString);

            _output.WriteLine(postResponseString);
            var deleteResponse = await Client.DeleteAsync($"{RequestUri}/{categoryDto.Category.CategoryId}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task When_UpdateCategoryCommandHandlerIsCalledWithRightParameters_Then_Success()
        {
            // Arrange
            var createResponse = await Client.PostAsJsonAsync(RequestUri, new CreateCategoryCommand
            {
                CategoryName = "Test Category"
            });
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<CategoryApiResponse<CategoryDto>>(createResponseString);

            var updateCommand = new UpdateCategoryCommand
            {
                CategoryId = createResult.Category.CategoryId,
                CategoryName = createResult.Category.CategoryName
            };

            // Act
            var updateResponse = await Client.PutAsJsonAsync($"{RequestUri}/{createResult.Category.CategoryId}", updateCommand);

            // Assert
            updateResponse.EnsureSuccessStatusCode();
            var updateResponseString = await updateResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<CategoryApiResponse<CategoryDto>>(updateResponseString);
            updateResult.Should().NotBeNull();
            updateResult.Category.Should().NotBeNull();
            updateResult.Category.CategoryId.Should().Be(updateCommand.CategoryId);
            // Add more assertions based on your requirements
        }

        [Fact]
        public async Task When_UpdateCategoryCommandHandlerIsCalledWithInvalidParameters_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            var createResponse = await Client.PostAsJsonAsync(RequestUri, new CreateCategoryCommand
            {
                CategoryName = "Test Category"
            });
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<CategoryApiResponse<CategoryDto>>(createResponseString);

            var updateCommand = new UpdateCategoryCommand
            {
                CategoryId = createResult.Category.CategoryId,
                CategoryName = ""
            };

            // Act
            var updateResponse = await Client.PutAsJsonAsync($"{RequestUri}/{createResult.Category.CategoryId}", updateCommand);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            // Add more assertions based on your requirements
        }

        [Fact]
        public async Task When_UpdateCategoryCommandHandlerIsCalledWithMismatchedCategoryIds_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            var createResponse = await Client.PostAsJsonAsync(RequestUri, new CreateCategoryCommand
            {
                CategoryName = "Test Category"
            });
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<CategoryApiResponse<CategoryDto>>(createResponseString);

            var updateCommand = new UpdateCategoryCommand
            {
                CategoryId = Guid.NewGuid(), // Mismatched CategoryId
                CategoryName = "Cat"
            };

            // Act
            var updateResponse = await Client.PutAsJsonAsync($"{RequestUri}/{createResult.Category.CategoryId}", updateCommand);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            // Add more assertions based on your requirements

            // Additional assertion to check if the response message is correct
            var responseContent = await updateResponse.Content.ReadAsStringAsync();
            responseContent.Should().Contain("The provided categoryId in the path does not match the one in the request body.");
        }

        [Fact]
        public async Task When_GetByIdCategoryQueryHandlerIsCalledWithExistingCategoryId_Then_Success()
        {
            // Arrange
            var createResponse = await Client.PostAsJsonAsync(RequestUri, new CreateCategoryCommand
            {
                CategoryName = "Test Category"
            });
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<CategoryApiResponse<CategoryDto>>(createResponseString);

            // Act
            var getByIdResponse = await Client.GetAsync($"{RequestUri}/{createResult.Category.CategoryId}");

            // Assert
            getByIdResponse.EnsureSuccessStatusCode();
            var getByIdResponseString = await getByIdResponse.Content.ReadAsStringAsync();
            var getByIdResult = JsonConvert.DeserializeObject<CategoryDto>(getByIdResponseString);
            getByIdResult.Should().NotBeNull();
            getByIdResult.CategoryId.Should().Be(createResult.Category.CategoryId);
            // Add more assertions based on your requirements
        }

    }
}
