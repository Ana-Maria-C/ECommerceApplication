using ECommerceApplication.App.Contracts;
using ECommerceApplication.App.Services.Responses;
using ECommerceApplication.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerceApplication.App.Services
{
    public class ApiResponsePhoto
    {
        public List<PhotoDto> Photos { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ValidationsErrors { get; set; }
    }

    public class PhotoDataService : IPhotoDataService
    {
        private const string RequestUri = "api/v1/Photos";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public PhotoDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<List<PhotoDto>>> GetPhotoForOwnerAsync(Guid ownerId)
        {
            var result = await httpClient.GetAsync($"{RequestUri}/{ownerId}");
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            Console.WriteLine($"Api response content: {content}");

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var apiResponse = JsonSerializer.Deserialize<ApiResponsePhoto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (apiResponse == null)
            {
                Console.WriteLine("Api response is null");
                throw new ApplicationException("Api response is null");
            }


            Console.WriteLine($"Mapped PhotoDto: {JsonSerializer.Serialize(apiResponse)}");


            return new ApiResponse<List<PhotoDto>>
            {
	            IsSuccess = apiResponse?.Success ?? false,
	            Message = apiResponse?.Message,
	            Data = apiResponse?.Photos
            };
        }

		// Add other photo-related methods as needed

		// Example method for uploading a photo
		public async Task<ApiResponse<PhotoDto>> UploadPhotoAsync(Guid ownerId, Stream photoStream, string fileName)
		{
			var formData = new MultipartFormDataContent();
			using var streamContent = new StreamContent(photoStream);
			formData.Add(streamContent, "Photo", fileName);
			formData.Add(new StringContent(ownerId.ToString()), "OwnerId");

            Console.WriteLine("Am ajuns aici.");

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsync("api/v1/Photos", formData);
			if (!result.IsSuccessStatusCode)
			{
				var errorResponse = await result.Content.ReadAsStringAsync();
				return new ApiResponse<PhotoDto> { IsSuccess = false, Message = errorResponse };
			}

			var response = await result.Content.ReadFromJsonAsync<ApiResponse<PhotoDto>>();
			response!.IsSuccess = result.IsSuccessStatusCode;
			return response!;
		}

		// Example method for deleting a photo
		public async Task<ApiResponse<PhotoDto>> DeletePhotoAsync(Guid photoId)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.DeleteAsync($"{RequestUri}/{photoId}");
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadFromJsonAsync<ApiResponse<PhotoDto>>();
        }

        // Add other methods for updating, searching, etc.
    }
}
