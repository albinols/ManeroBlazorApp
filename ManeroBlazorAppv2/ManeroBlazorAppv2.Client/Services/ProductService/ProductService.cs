using System.Linq;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using ManeroBlazorAppv2.Client.Models;

namespace ManeroBlazorAppv2.Client.Services.ProductService;

public class ProductService(HttpClient httpClient) : IProductService
{
	private readonly HttpClient _httpClient = httpClient;

	public async Task<Product[]> GetProductsAsync()
	{
		return await _httpClient.GetFromJsonAsync<Product[]>("https://maneroproductprovider.azurewebsites.net/api/products/getall?code=pizzakey");
	}

	public async Task<IEnumerable<Product>> GetFeaturedProducts()
	{
		var products = await GetProductsAsync();
		if (products != null)
		{
			var random = new Random();
			var featuredProducts = products.OrderBy(p => p.Name).Take(4);
			return featuredProducts;
		}
		return Enumerable.Empty<Product>();
	}

	public async Task<IEnumerable<Product>> GetAllProducts()
	{
		var products = await GetProductsAsync();
		if (products != null)
		{
			var allProducts = products.OrderBy(p => p.Name);
			return allProducts;
		}
		return Enumerable.Empty<Product>();
	}

	public async Task<IEnumerable<Product>> GetBestSellers()
	{
		var products = await GetProductsAsync();
		if (products != null)
		{
			var random = new Random();
			var bestSellers = products.OrderByDescending(p => p.Rating).Take(3);
			return bestSellers;
		}
		return Enumerable.Empty<Product>();
	}

	public async Task<IEnumerable<Product>> GetAllBestSellers()
	{
		var products = await GetProductsAsync();

		if (products != null)
		{
			var random = new Random();
			var bestSellers = products.OrderByDescending(p => p.Rating);
			return bestSellers;
		}
		return Enumerable.Empty<Product>();
	}

	public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
	{
		var products = await GetProductsAsync();

		return products.Where(p => p.Category == category);
	}

	public async Task<IEnumerable<Product>> GetProductsSorted(IEnumerable<Product> products, string sortBy)
	{
		switch (sortBy)
		{
			case "Rating":
				return products.OrderByDescending(p => p.Rating);
			case "PriceLowToHigh":
				return products.OrderBy(p => p.Price);
			case "PriceHighToLow":
				return products.OrderByDescending(p => p.Price);
			default:
				return products;
		}
	}

	public async Task<IEnumerable<ImageModel>> GetImageById(string Id)
	{
		var images = await httpClient.GetFromJsonAsync<IEnumerable<ImageModel>>($"https://productimageprovider.azurewebsites.net/api/product/images/{Id}?code=ImagesForImagesForImages");
		return images;
	}

	public async Task<Product> GetProductById(string Id)
	{
		var product = await httpClient.GetFromJsonAsync<Product>($"https://maneroproductprovider.azurewebsites.net/api/products/{Id}?code=PizzaKey");
		return product;
	}

	public async Task<IEnumerable<Product>> SearchProductByTitle(string searchParam)
	{
		return await _httpClient.GetFromJsonAsync<List<Product>>($"https://maneroproductprovider.azurewebsites.net/api/products/search/{searchParam}?code=SearchiSearch%0A");
	}

	public async Task<bool> DeleteProduct(string id)
	{
		var productDeleteUrl = "https://maneroproductprovider.azurewebsites.net/api/products/%7Bid%7D?code=ByebyeprodUct";
		var formattedUrl = productDeleteUrl.Replace("%7Bid%7D", id);
		var productDeleteUri = new Uri(formattedUrl);
		var result = await _httpClient.DeleteAsync(productDeleteUri);

		if (result.IsSuccessStatusCode)
		{
			return true;
		}
		return false;
	}

	//public async Task<bool> AddToCart(ShoppingCartItemModel model)
	//{
	//	var response = await _httpClient.PostAsJsonAsync("https://shoppingcartprovidermanero.azurewebsites.net/api/shoppingcart?code=7tK7C2_AjBwBGnLm-lHcoFpvREBTxO1qD4YIsc4kELykAzFujAgGrw%3D%3D", model);
	//	Console.WriteLine(response);
	//	return response.IsSuccessStatusCode;
	//}
}
