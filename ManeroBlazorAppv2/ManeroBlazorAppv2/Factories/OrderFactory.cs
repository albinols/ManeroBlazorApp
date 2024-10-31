using ManeroBlazorAppv2.Models;

namespace ManeroBlazorAppv2.Factories
{
    public class OrderFactory
    {
        public OrderRequestModel CreateOrderRequest(string userId, string shippingAddress, decimal deliveryCost, DateTime deliveryDate, List<OrderItem> orderItems, string maskedCardNumber, string paymentId)
        {
            return new OrderRequestModel
            {
                UserId = userId,
                Address = shippingAddress,
                DeliveryCost = deliveryCost,
                DeliveryDate = deliveryDate,
                MaskedCardNumber = maskedCardNumber,
                PaymentId = paymentId,
                OrderItem = orderItems
            };
        }
        public OrderItem CreateOrderItem(string productId, int quantity)
        {
            return new OrderItem
            {
                ProductId = productId,
                Quantity = quantity
            };
        }
    }
}
