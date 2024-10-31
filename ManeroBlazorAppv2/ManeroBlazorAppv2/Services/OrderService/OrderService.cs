using ManeroBlazorAppv2.Models;

namespace ManeroBlazorAppv2.Services.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly HttpClient _httpClient;

		public OrderService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<OrderResponseModel>> GetAllOrdersAsync(string userId)
		{
			var url = $"https://maneroorderprovider.azurewebsites.net/api/orders/{userId}?code=3it1b2na5nA_GK6jZWSCXk5pQ9MgHql6LUg_Wu0mHs9fAzFuKh3spQ%3D%3D";
			return await _httpClient.GetFromJsonAsync<List<OrderResponseModel>>(url);
		}

		public async Task<OrderResponseModel> GetOrderAsync(string orderId)
		{ 
            var url = $"https://maneroorderprovider.azurewebsites.net/api/order/{orderId}?code=9hYBRXmbHve0P4NEKW8SzbtJz6ZCtO-wEH3SrTKOih96AzFueiZlww%3D%3D";
			return await _httpClient.GetFromJsonAsync<OrderResponseModel>(url);
		}
	}
}
