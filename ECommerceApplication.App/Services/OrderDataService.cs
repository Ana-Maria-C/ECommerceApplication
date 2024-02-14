using ECommerceApplication.App.Contracts;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace ECommerceApplication.App.Services
{
    public class OrderDataService : IOrderDataService
    {
        private const string RequestUri = "api/v1/Order";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly IOrderItemDataService orderItemDataService;

        public OrderDataService(HttpClient httpClient, ITokenService tokenService, IOrderItemDataService orderItemDataService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.orderItemDataService = orderItemDataService;
        }

        //public async Task<OrderViewModel> GetOrderByIdAsync(Guid orderId)
        //{
        //    var response = await httpClient.GetAsync($"{RequestUri}/{orderId}", HttpCompletionOption.ResponseHeadersRead);
        //    response.EnsureSuccessStatusCode();
        //    var content = await response.Content.ReadAsStringAsync();
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new ApplicationException(content);
        //    }
        //    Console.WriteLine(content);
        //    var apiResponse = JsonSerializer.Deserialize<OrderViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //    if (apiResponse == null)
        //    {
        //        throw new ApplicationException("Api response is null");
        //    }
        //    return apiResponse!;
        //}

        public async Task<OrderViewModel> Create(Guid shoppingCartId, Guid addressId)
        {

            var createOrderDto = new CreateOrderDto
            {
                ShoppingCartId = shoppingCartId,
                AddressId = addressId
            };

            httpClient.DefaultRequestHeaders.Authorization
                 = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync($"{RequestUri}", createOrderDto);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(result.ReasonPhrase);
            }
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<OrderViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!.Data;

        }
        public async Task<List<OrderViewModel>> GetByUserId(Guid userId) {
            var response = await httpClient.GetAsync($"{RequestUri}?userId={userId.ToString()}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ApiResponse<List<OrderViewModel>>>();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            Console.WriteLine(content);
            //var apiResponse = JsonSerializer.Deserialize<ApiResponse<ShoppingCartViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (content == null || content.IsSuccess)
            {
                Console.WriteLine("Api response is null");
                throw new ApplicationException("Api response is null");
            }
            return content.Data;
        }
    }
}
