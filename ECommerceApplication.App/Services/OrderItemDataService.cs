using ECommerceApplication.App.Contracts;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ECommerceApplication.App.Services
{
    public class OrderItemDataService : IOrderItemDataService
    {
        private const string RequestUri = "api/v1/orderitems";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public OrderItemDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        
        public async Task<ApiResponse<OrderItemDto>> CreateOrderItemAsync(OrderItemViewModel orderItemViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, orderItemViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<OrderItemDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<List<OrderItemViewModel>> GetOrderItemsAsync()
        {
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine(content);
            var orderItems = JsonSerializer.Deserialize<List<OrderItemViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return orderItems!;
        }

        public async Task<ApiResponse<OrderItemDto>> UpdateOrderItemAsync(OrderItemViewModel updatedOrderItem)
        {
            // Asigură-te că ai o rută corectă definită în backend pentru a actualiza categoria
            string updateUri = $"api/v1/orderitems/{updatedOrderItem.OrderItemId}";

            // Setează token-ul de autorizare pentru a putea accesa ruta protejată din backend
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            // Trimite o solicitare PUT către backend pentru a actualiza categoria
            var result = await httpClient.PutAsJsonAsync(updateUri, updatedOrderItem);

            // Asigură-te că solicitarea a fost realizată cu succes
            result.EnsureSuccessStatusCode();

            // Citește răspunsul JSON primit de la backend și parsează-l într-un obiect ApiResponse<OrderItemDto>
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<OrderItemDto>>();

            // Setează proprietatea IsSuccess pe baza rezultatului solicitării HTTP
            response!.IsSuccess = result.IsSuccessStatusCode;

            return response!;
        }


        public async Task<ApiResponse<OrderItemDto>> AddOrderItemToCartAsync(OrderItemViewModel orderItemViewModel)
        {
            string requestUri = "api/v1/orderitems/addtocart";

            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.PostAsJsonAsync(requestUri, orderItemViewModel);

            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<OrderItemDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;

            return response!;
        }

        public async Task<ApiResponse<OrderItemDto>> RemoveItemFromCartAsync(Guid orderItemId)
        {
            string requestUri = $"{RequestUri}/{orderItemId.ToString()}";
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var response = await httpClient.DeleteAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<OrderItemDto>>();

            result!.IsSuccess = response.IsSuccessStatusCode;

            return result!;
        }

        public async Task<List<OrderItemViewModel>> getByShoppingCartIdAndProductId(Guid shoppingCartId, Guid productId) { 
            string requestUri = $"{RequestUri}?shoppingCartId={shoppingCartId.ToString()}&productId={productId.ToString()}";

            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var response = await httpClient.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<OrderItemViewModel>>>();

            result!.IsSuccess = response.IsSuccessStatusCode;

            return result!.Data;
        }

    }
}
