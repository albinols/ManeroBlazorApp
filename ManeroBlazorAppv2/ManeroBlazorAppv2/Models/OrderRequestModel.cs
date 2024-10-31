namespace ManeroBlazorAppv2.Models
{
    public class OrderRequestModel
    {
        public string UserId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal DeliveryCost { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string MaskedCardNumber { get; set; } = null!;
        public string PaymentId { get; set; } = null!;
        public List<OrderItem> OrderItem { get; set; }
    }

    public class OrderItem
    {
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
