using ECommerceApplication.App.Contracts;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ECommerceApplication.App.Services
{
    public class ApiResponseReviews
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<String> ValidationsErrors { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
    public class ReviewDataService:IReviewDataService
    {
        private const string RequestUri = "api/v1/reviews";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ReviewDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<ReviewDto>> CreateReviewAsync(ReviewViewModel reviewViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, reviewViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ReviewDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<List<ReviewViewModel>> GetReviewAsync()
        {
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine(content);
            var apiResponse = JsonSerializer.Deserialize<ApiResponseReviews>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return apiResponse.Reviews;
        }

        public async Task<List<ReviewViewModel>> GetReviewByProductId(Guid productId)
        {
            string requestUri = $"{RequestUri}/ByProductId/{productId}";

            // Adaugă token-ul de autorizare la cerere
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            // Realizează cererea HTTP GET
            var result = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            // Citește conținutul răspunsului
            var content = await result.Content.ReadAsStringAsync();

            // Verifică dacă cererea a fost reușită
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            Console.WriteLine(content);
            // Deserializare JSON în lista de ReviewViewModel
            if (string.IsNullOrEmpty(content) || content.Trim() == "[]")
            {
                return new List<ReviewViewModel>();
            }
            var review = JsonSerializer.Deserialize<List<ReviewViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return review;
        }

        public async Task<ApiResponse<ReviewDto>> DeleteReviewAsync(Guid reviewId)
        {
            string deleteUri = $"{RequestUri}/{reviewId}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.DeleteAsync(deleteUri);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ReviewDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
    }
}
