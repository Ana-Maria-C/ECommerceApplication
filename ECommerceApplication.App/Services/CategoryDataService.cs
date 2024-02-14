using ECommerceApplication.App.Contracts;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ECommerceApplication.App.Services
{
    public class ApiResponse
    {
        public List<CategoryViewModel> Categories { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<String> ValidationsErrors { get; set; }
    }

    public class CategoryDataService : ICategoryDataService
    {
        private const string RequestUri = "api/v1/categories";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public CategoryDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<CategoryDto>> CreateCategoryAsync(CategoryViewModel categoryViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, categoryViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<CategoryDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            Console.WriteLine(content);
            var categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return categories!;
        }

        public async Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(CategoryViewModel updatedCategory)
        {
            string updateUri = $"api/v1/categories/{updatedCategory.CategoryId}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PutAsJsonAsync(updateUri, updatedCategory);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<CategoryDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;

            return response!;
        }

    }
}
