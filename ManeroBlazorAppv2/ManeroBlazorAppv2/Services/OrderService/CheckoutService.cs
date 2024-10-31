using ManeroBlazorAppv2.Models;

namespace ManeroBlazorAppv2.Services.OrderService
{
    public class CheckoutService
    {
        public string UserId { get; set; } = null!;
        public string ShippingAddress { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string MaskedCardNumber { get; set; } = null!;
        public string PaymentId { get; set; } = null!;
        public OrderResponseModel CurrentOrder { get; set; }

        public void ResetCheckout()
        {
            UserId = null;
            ShippingAddress = null;
            PaymentMethod = null;
            MaskedCardNumber = null;
            PaymentId = null;
            CurrentOrder = null;
        }
    }
}
