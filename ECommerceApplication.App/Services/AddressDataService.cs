using ECommerceApplication.App.Contracts;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ECommerceApplication.App.Services
{
    public class ApiResponseAddress
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<String> ValidationsErrors { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
    }
    public class AddressDataService : IAddressDataService
    {
        private const string RequestUri = "api/v1/addresses";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public AddressDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<AddressDto>> CreateAddressAsync(AddressViewModel addressViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, addressViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<AddressDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<List<AddressViewModel>> GetAddressesAsync()
        {
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine(content);
            var apiResponse = JsonSerializer.Deserialize<ApiResponseAddress>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            List<AddressViewModel> addresses = apiResponse.Addresses;
            return addresses!;
        }

        public async Task<ApiResponse<AddressDto>> UpdateAddressAsync(AddressViewModel updatedAddress)
        {
            string updateUri = $"api/v1/addresses/{updatedAddress.AddressId}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PutAsJsonAsync(updateUri, updatedAddress);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<AddressDto>>();
           // response!.IsSuccess = result.IsSuccessStatusCode;

            return response;
        }


        public async Task<List<AddressViewModel>> GetUserAddressesAsync(Guid userId)
        {
            try
            {
                var uri = $"{RequestUri}/ByUserId/{userId}";
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
                var result = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                result.EnsureSuccessStatusCode();
                var content = await result.Content.ReadAsStringAsync();

                if (!result.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var addresses = JsonSerializer.Deserialize<AddressesResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return addresses?.Addresses ?? new List<AddressViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during deserialization: {ex}");
                throw;
            }
        }

        public async Task<ApiResponse<AddressDto>> DeleteAddressAsync(Guid addressId)
        {
            string deleteUri = $"api/v1/addresses/{addressId}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.DeleteAsync(deleteUri);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<AddressDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;

            return response!;
        }   
    }


    public class AddressesResponse
    {
        public List<AddressViewModel>? Addresses { get; set; }
    }
}
