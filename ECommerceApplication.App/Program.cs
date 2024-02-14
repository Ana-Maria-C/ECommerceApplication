using Blazored.LocalStorage;
using ECommerceApplication.App.Auth;
using ECommerceApplication.App.Contracts;
using ECommerceApplication.App;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using ECommerceApplication.App.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddHttpClient<ICategoryDataService, CategoryDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});
builder.Services.AddHttpClient<IProductDataService, ProductDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});
builder.Services.AddHttpClient<IAddressDataService, AddressDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});
builder.Services.AddHttpClient<IOrderItemDataService, OrderItemDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});
builder.Services.AddHttpClient<IShoppingCartDataService, ShoppingCartDataService>(client=>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});
builder.Services.AddHttpClient<IUserDataService, UserDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});
builder.Services.AddHttpClient<IReviewDataService, ReviewDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});
builder.Services.AddHttpClient<IPhotoDataService, PhotoDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");    
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});
builder.Services.AddHttpClient<IOrderDataService, OrderDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/");
});

await builder.Build().RunAsync();
