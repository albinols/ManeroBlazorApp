using ManeroBlazorAppv2.Models;

namespace ManeroBlazorAppv2.Services.OrderService
{
	public interface IOrderService
	{
		Task<List<OrderResponseModel>> GetAllOrdersAsync(string userId);
        Task<OrderResponseModel> GetOrderAsync(string orderId);
    }
}