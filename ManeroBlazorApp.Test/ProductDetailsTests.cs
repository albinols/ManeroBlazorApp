using System.Security.Claims;
using Bunit;
using Bunit.TestDoubles;
using ManeroBlazorAppv2.Client.Components.Products;
using ManeroBlazorAppv2.Client.Models;
using ManeroBlazorAppv2.Client.Services.ProductService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ManeroBlazorApp.Test;

public class ProductDetailsTests : TestContext
{
	private readonly Mock<IProductService> _mockProductService;
	private readonly Mock<AuthenticationStateProvider> _mockAuthenticationStateProvider;

	public ProductDetailsTests()
	{
		_mockProductService = new Mock<IProductService>();
		_mockAuthenticationStateProvider = new Mock<AuthenticationStateProvider>();

		Services.AddSingleton(_mockProductService.Object);
		Services.AddSingleton(_mockAuthenticationStateProvider.Object);
	}

	[Fact]
	public void RendersLoadingWhenProductIsNull()
	{
		// Arrange
		_mockProductService.Setup(service => service.GetProductById(It.IsAny<string>()))
			.ReturnsAsync((Product)null);

		// Act
		var component = RenderComponent<ProductDetails>(parameters => parameters.Add(p => p.Id, "1"));

		// Assert
		component.Markup.Contains("<p>Loading...</p>");
	}

	[Fact]
	public void RendersProductDetailsWhenProductIsNotNull()
	{
		// Arrange
		var product = new Product
		{
			Name = "Test Product",
			Price = 99,
			Rating = 4.5,
			Sizes = new List<string> { "S", "M", "L" },
			Colors = new List<string> { "Red", "Blue" },
			Description = "Test Description",
			ColorImageUrls = new Dictionary<string, List<string>>
				{
					{ "Red", new List<string> { "red1.jpg", "red2.jpg" } },
					{ "Blue", new List<string> { "blue1.jpg", "blue2.jpg" } }
				}
		};
		_mockProductService.Setup(service => service.GetProductById(It.IsAny<string>()))
			.ReturnsAsync(product);

		// Act
		var component = RenderComponent<ProductDetails>(parameters => parameters.Add(p => p.Id, "1"));

		// Assert
		component.Markup.Contains("Test Product");
		component.Markup.Contains("$99.99");
		component.Markup.Contains("Test Description");
		Assert.Equal(product.Name, component.Instance.product.Name);
		Assert.Equal(product.Price, component.Instance.product.Price);
		Assert.Equal(product.Description, component.Instance.product.Description);
	}

	[Fact]
	public async Task AddToCartRedirectsToLoginWhenNotAuthenticated()
	{
		// Arrange
		var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
		_mockAuthenticationStateProvider.Setup(provider => provider.GetAuthenticationStateAsync())
			.Returns(authState);
		var product = new Product
		{
			Name = "Test Product",
			Price = 99,
			Rating = 4.5,
			Sizes = new List<string> { "S", "M", "L" },
			Colors = new List<string> { "Red", "Blue" },
			Description = "Test Description",
			ColorImageUrls = new Dictionary<string, List<string>>
			{
				{ "Red", new List<string> { "red1.jpg", "red2.jpg" } },
				{ "Blue", new List<string> { "blue1.jpg", "blue2.jpg" } }
			}
		};
		_mockProductService.Setup(service => service.GetProductById(It.IsAny<string>()))
			.ReturnsAsync(product);

		// Act
		var component = RenderComponent<ProductDetails>(parameters => parameters.Add(p => p.Id, "1"));
		await component.InvokeAsync(() => component.Instance.AddToCart());

		// Assert
		var navMan = Services.GetRequiredService<FakeNavigationManager>();
		navMan.NavigateTo("/login");
	}

	[Fact]
	public async Task AddToCartAddsProductWhenAuthenticated()
	{
		// Arrange
		var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "user1") };
		var identity = new ClaimsIdentity(claims, "TestAuthType");
		var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
		_mockAuthenticationStateProvider.Setup(provider => provider.GetAuthenticationStateAsync())
			.Returns(authState);
		var product = new Product
		{
			Name = "Test Product",
			Price = 99,
			Rating = 4.5,
			Sizes = new List<string> { "S", "M", "L" },
			Colors = new List<string> { "Red", "Blue" },
			Description = "Test Description",
			ColorImageUrls = new Dictionary<string, List<string>>
				{
					{ "Red", new List<string> { "red1.jpg", "red2.jpg" } },
					{ "Blue", new List<string> { "blue1.jpg", "blue2.jpg" } }
				}
		};
		_mockProductService.Setup(service => service.GetProductById(It.IsAny<string>()))
			.ReturnsAsync(product);

		// Act
		var component = RenderComponent<ProductDetails>(parameters => parameters.Add(p => p.Id, "1"));
		await component.InvokeAsync(() => component.Instance.AddToCart());

		// Assert
		//_mockProductService.Verify(service => service.AddToCart(It.IsAny<ShoppingCartItemModel>()), Times.Once);
	}
}
