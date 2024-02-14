using ECommerceApplication.App.Contracts;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;
using System.Data;
using System.Diagnostics.Contracts;
using System.Net.Http.Json;
using System.Text.Json;

namespace ECommerceApplication.App.Services
{
    public class ApiResponseShoppingCart
    {
        public List<ShoppingCartViewModel> ShoppingCarts { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<String> ValidationsErrors { get; set; }
    }
    public class ShoppingCartDataService : IShoppingCartDataService
    {
        private const string RequestUri = "api/v1/ShoppingCart";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly IOrderItemDataService orderItemDataService;

        public ShoppingCartDataService(HttpClient httpClient, ITokenService tokenService, IOrderItemDataService orderItemDataService)
        {
            this.orderItemDataService = orderItemDataService;
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ShoppingCartViewModel> GetShoppingCartByIdAsync(Guid shoppingCartId)
        {
            var response = await httpClient.GetAsync($"{RequestUri}/{shoppingCartId}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine(content);
            var apiResponse = JsonSerializer.Deserialize<ShoppingCartViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (apiResponse == null)
            {
                throw new ApplicationException("Api response is null");
            }
            return apiResponse!;
        }
        public async Task<List<ShoppingCartViewModel>> GetShoppingCartsAsync()
        {
            var response = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine(content);
            //var shoppingCart = JsonSerializer.Deserialize<List<ShoppingCartViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //var jsonDocument = JsonDocument.Parse(content);
            //var shoppingCartsElement = jsonDocument.RootElement.GetProperty("shoppingCarts");
            //var shoppingCart = JsonSerializer.Deserialize<List<ShoppingCartViewModel>>(shoppingCartsElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var apiResponse = JsonSerializer.Deserialize<ApiResponseShoppingCart>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (apiResponse == null)
            {
                throw new ApplicationException("Api response is null");
            }
            List<ShoppingCartViewModel> shoppingCarts = apiResponse.ShoppingCarts;
            return shoppingCarts!;
        }
        public async Task<ShoppingCartViewModel> GetShoppingCartByUserIdAsync(Guid userId)
        {
            var response = await httpClient.GetAsync($"{RequestUri}?userId={userId.ToString()}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ApiResponse<ShoppingCartViewModel>>();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            Console.WriteLine(content);
            //var apiResponse = JsonSerializer.Deserialize<ApiResponse<ShoppingCartViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (content == null)
            {
                Console.WriteLine("Api response is null");
                throw new ApplicationException("Api response is null");
            }
            return content.Data;
        }

        public async Task<ShoppingCartViewModel> AttachOrderItemById(Guid shoppingCartId, OrderItemDto orderItem)
        {
            var orderItems = await orderItemDataService.getByShoppingCartIdAndProductId(shoppingCartId, orderItem.ProductId);
            if (orderItems.Count <= 0)
            {
                var response = await httpClient.PutAsync($"{RequestUri}/{shoppingCartId}/AddItem/{orderItem.OrderItemId}", null);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }
                var apiResponse = JsonSerializer.Deserialize<ShoppingCartViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResponse == null)
                {
                    throw new ApplicationException("Api response is null");
                }
                return apiResponse!;
            }
            OrderItemViewModel duplicateOrderItem = orderItems[0];
            duplicateOrderItem.Quantity += orderItem.Quantity;
            orderItemDataService.UpdateOrderItemAsync(duplicateOrderItem);
            return null;

        }

    }
}
