using ManeroBlazorAppv2.Client.Models;

namespace ManeroBlazorAppv2.Client.Services.ProductService
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetAllBestSellers();
		Task<IEnumerable<Product>> GetAllProducts();
		Task<IEnumerable<Product>> GetBestSellers();
		Task<IEnumerable<Product>> GetFeaturedProducts();
		Task<IEnumerable<ImageModel>> GetImageById(string Id);
		Task<Product> GetProductById(string Id);
		Task<Product[]> GetProductsAsync();
		Task<IEnumerable<Product>> GetProductsByCategory(string category);
		Task<IEnumerable<Product>> GetProductsSorted(IEnumerable<Product> products, string sortBy);
		Task<IEnumerable<Product>> SearchProductByTitle(string searchParam);
		Task<bool> DeleteProduct(string productId);
	}
}
