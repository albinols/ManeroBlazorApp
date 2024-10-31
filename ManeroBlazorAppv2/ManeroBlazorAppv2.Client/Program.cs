using Blazored.LocalStorage;
using ManeroBlazorAppv2.Client;
using ManeroBlazorAppv2.Client.Services.AddressService;
using ManeroBlazorAppv2.Client.Services.ProductService;
using ManeroBlazorAppv2.Services.ShoppingCartService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ShoppingCartService>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
