using FluentAssertions;
using ECommerceApplication.API.IntegrationTests.Base;
using ECommerceApplication.Application.Features.Addresses.Queries;
using ECommerceApplication.Application.Features.Addresses.Commands.CreateAddress;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using ECommerceApplication.App.Services;
using ECommerceApplication.Application.Features.Categories.Commands.CreateCategory;
using System.Net;
using ECommerceApplication.Api.IntegrationTests.Controllers;
using ECommerceApplication.Application.Features.Addresses.Commands.UpdateAddress;
using Xunit.Abstractions;

namespace ECommerceApplication.API.IntegrationTests.Controllers
{

    public class AddressesControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/addresses";

        private readonly ITestOutputHelper _output;

        public AddressesControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task When_GetAllAddressesQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<App.Services.Responses.ApiResponse<List<AddressDto>>>(responseString);
            // Assert
            result?.Data?.Count.Should().Be(2);
        }

        [Fact]
        public async Task When_PostAddressCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange
            var address = new CreateAddressCommand
            {
                Street = "Test Street",
                City = "Test City",
                State = "Test State",
                PostalCode = "Test PostalCode",
                IsDefault = true
            };
            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, address);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AddressApiResponse<AddressDto>>(responseString);
            result?.Should().NotBeNull();
            
            result?.Address?.Street.Should().Be(address.Street);
        }

        [Fact]
        public async Task When_PostAddressCommandHandlerIsCalledWithInvalidParameters_Then_BadRequestShouldBeReturned()
        {
            var address = new CreateAddressCommand
            {
                State = ""
            };
            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, address);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task When_UpdateAddressCommandHandlerIsCalledWithValidParameters_Then_Success()
        {
            // Arrange
            var createResponse = await Client.PostAsJsonAsync(RequestUri, new CreateAddressCommand
            {
                Street = "Street",
                City = "City",
                State = "State",
                PostalCode = "PostalCode",
                IsDefault = false
            });
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<AddressApiResponse<AddressDto>>(createResponseString);

            var updateCommand = new UpdateAddressCommand
            {
                AddressId = createResult.Address.AddressId,
                Street = "Updated Street",
                City = "Updated City",
                State = "Updated State",
                PostalCode = "Updated PostalCode",
                IsDefault = false
            };

            // Act
            var response = await Client.PutAsJsonAsync($"{RequestUri}/{createResult.Address.AddressId}", updateCommand);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AddressApiResponse<AddressDto>>(responseString);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task When_UpdateAddressCommandHandlerIsCalledWithMismatchedIds_Then_BadRequestShouldBeReturned()
        {
            // Arrange
            var createResponse = await Client.PostAsJsonAsync(RequestUri, new CreateAddressCommand
            {
                Street = "Street",
                City = "City",
                State = "State",
                PostalCode = "PostalCode",
                IsDefault = false
            });
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<AddressApiResponse<AddressDto>>(createResponseString);

            var updateCommand = new UpdateAddressCommand
            {
                AddressId = Guid.NewGuid(),
                Street = "Updated Street",
                City = "Updated City",
                State = "Updated State",
                PostalCode = "Updated PostalCode",
                IsDefault = false
            };

            // Act
            var response = await Client.PutAsJsonAsync($"{RequestUri}/{createResult.Address.AddressId}", updateCommand);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be("The provided product ID does not match the request body.");
        }

        [Fact]
        public async Task When_UpdateAddressCommandHandlerFails_Then_NotFoundShouldBeReturned()
        {
            // Arrange
            // Arrange
            var createResponse = await Client.PostAsJsonAsync(RequestUri, new CreateAddressCommand
            {
                Street = "Street",
                City = "City",
                State = "State",
                PostalCode = "PostalCode",
                IsDefault = false
            });
            createResponse.EnsureSuccessStatusCode();
            var createResponseString = await createResponse.Content.ReadAsStringAsync();
            var createResult = JsonConvert.DeserializeObject<AddressApiResponse<AddressDto>>(createResponseString);

            var updateCommand = new UpdateAddressCommand
            {
                AddressId = createResult.Address.AddressId,
                Street = "Updated Street"
            };

            // Assume the update fails, for example, due to validation errors
            // Simulate a failure scenario in your application logic or mock the Mediator behavior

            // Act
            var response = await Client.PutAsJsonAsync($"{RequestUri}/{createResult.Address.AddressId}", updateCommand);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
