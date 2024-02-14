using System.Drawing.Printing;
using FluentAssertions;
using ECommerceApplication.API.IntegrationTests.Base;
using Newtonsoft.Json;
using System.Net.Http.Json;
using ECommerceApplication.Api.IntegrationTests.Controllers;
using ECommerceApplication.Application.Features;
using Xunit.Abstractions;
using ECommerceApplication.Application.Features.OrderItems;
using ECommerceApplication.Application.Features.OrderItems.Commands.CreateOrderItem;
using ECommerceApplication.Application.Features.Products.Queries;

namespace ECommerceApplication.API.IntegrationTests.Controllers
{

    public class OrderItemsControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/orderitems";
        private const string RequestProductUri = "/api/v1/products";
        private readonly ITestOutputHelper _output;

        public OrderItemsControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public async Task When_GetAllOrderItemsQueryHandlerIsCalled_Then_Success()
        {
            // Arrange && Act
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<OrderItemDto>>(responseString);
            // Assert
            result?.Count.Should().Be(2);
        }
        
        [Fact]
        public async Task When_PostOrderItemsCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            // Arrange and Act
            var responseProduct = await Client.GetAsync(RequestProductUri);

            responseProduct.EnsureSuccessStatusCode();

            var responseProductString = await responseProduct.Content.ReadAsStringAsync();

            _output.WriteLine(responseProductString);

            var resultProduct = JsonConvert.DeserializeObject<ApiResponse<List<ProductDto>>>(responseProductString);

            if (resultProduct == null)
            {
                // Handle the error appropriately.
                // For example, you might want to throw an informative exception,
                // log an error message, or simply return.
                throw new Exception("Product data is not available or insufficient.");
            }
            
            var orderItem = new CreateOrderCommand
            {
                ProductId = resultProduct.Products[0].ProductId,
                Quantity = 4,
                PricePerUnit = 200
            };

            // Act
            var response = await Client.PostAsJsonAsync(RequestUri, orderItem);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<OrderItemDto>>(responseString);
            result?.Should().NotBeNull();
            result?.Products?.ProductId.Should().Be(orderItem.ProductId);
            result?.Products?.Quantity.Should().Be(orderItem.Quantity);
            result?.Products?.PricePerUnit.Should().Be(orderItem.PricePerUnit);
        }



    }
}
