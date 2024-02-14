using ECommerceApplication.App.Contracts;
using ECommerceApplication.App.Pages;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ECommerceApplication.App.Services
{
    public class ApiResponseProduct
    {
        public List<ProductViewModel> Products { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<String> ValidationsErrors { get; set; }
    }
    public class ProductDataService : IProductDataService
    {
        private const string RequestUri = "api/v1/Products";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ProductDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponseForProduct> CreateProductAsync(ProductViewModel productViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, productViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponseForProduct>();
            response!.Success = result.IsSuccessStatusCode;
            Console.Write("Product id: ");
            Console.WriteLine(response.Product.ProductId);
            return response!;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(Guid productId)
        {
            var result = await httpClient.GetAsync($"{RequestUri}/{productId}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            Console.WriteLine($"Api response content: {content}");

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var apiResponse = JsonSerializer.Deserialize<ProductDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (apiResponse == null)
            {
                Console.WriteLine("Api response is null");
                throw new ApplicationException("Api response is null");
            }

            Console.WriteLine($"Mapped ProductDto: {JsonSerializer.Serialize(apiResponse)}");

            var productViewModel = new ProductViewModel
            {
                ProductId = apiResponse.ProductId,
                ProductName = apiResponse.ProductName,
                Description = apiResponse.Description,
                ImageUrl = apiResponse.ImageUrl,
                Price = apiResponse.Price,
                CategoryId = apiResponse.CategoryId,
                Category = new CategoryViewModel
                {
                    CategoryId = apiResponse.Category.CategoryId,
                    CategoryName = apiResponse.Category.CategoryName
                }
            };


            Console.WriteLine($"Mapped ProductViewModel: {JsonSerializer.Serialize(productViewModel)}");

            return productViewModel;
        }

        public async Task<List<ProductViewModel>> GetProductAsync()
        {
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            //Console.WriteLine(content);
            var apiResponse = JsonSerializer.Deserialize<ApiResponseProduct>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if(apiResponse == null)
            {
                throw new ApplicationException("Api response is null");
            }
            Console.WriteLine(apiResponse);
            List<ProductViewModel> products = apiResponse.Products;
            return products!;
        }

        public async Task<ApiResponse<ProductDto>> UpdateProductAsync(ProductViewModel updatedProduct)
        {
            // Asigură-te că ai o rută corectă definită în backend pentru a actualiza categoria

            string updateUri = $"{RequestUri}/{updatedProduct.ProductId.ToString().ToUpper()}"; 

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PutAsJsonAsync(updateUri, updatedProduct);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<List<ProductViewModel>> SearchProductsAsync(string searchQuery)
        {
            var searchUri = $"{RequestUri}/search?query={searchQuery}";

            var result = await httpClient.GetAsync(searchUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            Console.WriteLine($"Api response content: {content}");

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var apiResponse = JsonSerializer.Deserialize<ApiResponseProduct>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (apiResponse == null)
            {
                Console.WriteLine("Api response is null");
                throw new ApplicationException("Api response is null");
            }

            Console.WriteLine($"Mapped ApiResponseProduct: {JsonSerializer.Serialize(apiResponse)}");

            var products = apiResponse.Products.Select(productDto => new ProductViewModel
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                ImageUrl = productDto.ImageUrl,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                Category = new CategoryViewModel
                {
                    CategoryId = productDto.Category.CategoryId,
                    CategoryName = productDto.Category.CategoryName
                }
            }).ToList();

            Console.WriteLine($"Mapped ProductViewModels: {JsonSerializer.Serialize(products)}");

            return products;
        }

    }
}
