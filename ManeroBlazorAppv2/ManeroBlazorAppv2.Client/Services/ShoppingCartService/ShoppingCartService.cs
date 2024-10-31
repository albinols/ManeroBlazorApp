using Blazored.LocalStorage;
using ManeroBlazorAppv2.Client.Components.Pages.ShoppingCart;
using ManeroBlazorAppv2.Client.Models;
using Microsoft.AspNetCore.Http;

namespace ManeroBlazorAppv2.Services.ShoppingCartService
{
	public class ShoppingCartService
	{
		private readonly ILocalStorageService _localStorage;

		public ShoppingCartService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ILocalStorageService localStorage)
		{
			_localStorage = localStorage;
		}
		public async Task<List<ShoppingCartItemModel>> AddToCartAsync(ShoppingCartItemModel model)
		{
			try
			{
				var shoppingCart = await GetShoppingCartAsync(model.CustomerId) ?? new List<ShoppingCartItemModel>();
				var existingItem = shoppingCart.FirstOrDefault(i => i.ProductId == model.ProductId);

				if (existingItem != null)
				{
					shoppingCart.Remove(existingItem);
				}

				shoppingCart!.Add(model);
				await _localStorage.SetItemAsync(model.CustomerId, shoppingCart);
				return shoppingCart;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occured. {ex.Message}");
				return new List<ShoppingCartItemModel>();
			}
		}

		public async Task<List<ShoppingCartItemModel>> GetShoppingCartAsync(string customerId)
		{
			try
			{
				var shoppingCart = await _localStorage.GetItemAsync<List<ShoppingCartItemModel>>(customerId);
				return shoppingCart;
			}

			catch (Exception ex)
			{
				Console.WriteLine($"An error occured. {ex.Message}");
				return new List<ShoppingCartItemModel>();
			}
		}

		public async Task<List<ShoppingCartItemModel>> DeleteShoppingCartItem(string customerId, int productIndex)
		{
			try
			{
				var shoppingCart = await GetShoppingCartAsync(customerId) ?? new List<ShoppingCartItemModel>();
				shoppingCart.RemoveAt(productIndex);
				await _localStorage.SetItemAsync(customerId, shoppingCart);
				return shoppingCart;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occured. {ex.Message}");
				return null;
			}
		}
		public async Task<List<ShoppingCartItemModel>> DeleteAllShoppingCartItems(string customerId)
		{
			try
			{
				await _localStorage.RemoveItemAsync(customerId);
				return new List<ShoppingCartItemModel>();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred. {ex.Message}");
				return null;
			}
		}
	}
}

