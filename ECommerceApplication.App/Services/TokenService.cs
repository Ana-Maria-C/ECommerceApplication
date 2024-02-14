using Blazored.LocalStorage;
using ECommerceApplication.App.Contracts;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerceApplication.App.Services
{

    public class TokenService : ITokenService
    {
        private const string TOKEN = "token";
        private readonly ILocalStorageService localStorageService;

        public TokenService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public Task<string> DecodeEmailFromTokenAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            return Task.FromResult(email);
        }

        public Task<string> DecodeUsernameFromTokenAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var username = jsonToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            return Task.FromResult(username);
        }
        public async Task SetTokenAsync(string token)
        {
            await localStorageService.SetItemAsync(TOKEN, token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await localStorageService.GetItemAsync<string>(TOKEN);
        }

        public async Task RemoveTokenAsync()
        {
            await localStorageService.RemoveItemAsync(TOKEN);
        }
        public Task<Guid> DecodeUserIdFromTokenAsync(string token) {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var id = jsonToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            return Task.FromResult(new Guid(id));
        }
    }
}
