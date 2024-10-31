using ManeroBlazorAppv2.Client.Services.AddressService;
using ManeroBlazorAppv2.Client.Services.ProductService;
using ManeroBlazorAppv2.Components;
using ManeroBlazorAppv2.Components.Account;
using ManeroBlazorAppv2.Data;
using ManeroBlazorAppv2.Services;
using ManeroBlazorAppv2.Services.PaymentService;
using ManeroBlazorAppv2.Services.OrderService;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ManeroBlazorAppv2.Factories;
using ManeroBlazorAppv2.Services.ShoppingCartService;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<ShoppingCartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ProductImageService>();


builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<CheckoutService>();
builder.Services.AddScoped<OrderFactory>();
builder.Services.AddScoped<ShoppingCartService>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddBlazorBootstrap();

builder.Services.AddBlazoredLocalStorage();



builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;

    })
    .AddIdentityCookies();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer("Server=tcp:manero-userdb.database.windows.net,1433;Initial Catalog=manero-userdb;Persist Security Info=False;User ID=Sqladmin;Password=Bytmig123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnection")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(option =>
option.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ManeroBlazorAppv2.Client._Imports).Assembly);

app.MapAdditionalIdentityEndpoints();

app.MapPost("/Logout", async (
    HttpContext httpContext,
    SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.LocalRedirect("/");
});

app.Run();
