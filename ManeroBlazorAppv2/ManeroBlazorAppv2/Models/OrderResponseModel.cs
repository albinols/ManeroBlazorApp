namespace ManeroBlazorAppv2.Models
{
	public class OrderResponseModel
	{
		public string OrderId { get; set; } = null!;
		public string UserId { get; set; } = null!;
		public string Address { get; set; } = null!;
		public decimal DeliveryCost { get; set; }
		public DateTime DeliveryDate { get; set; }
		public decimal TotalAmount { get; set; }
		public string OrderStatus { get; set; } = null!;
		public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; } = null!;
        public string PaymentId { get; set; } = null!;
        public string MaskedCardNumber { get; set; } = null!;
        public List<OrderResponseItem> OrderItems { get; set; } = new List<OrderResponseItem>();

    }
	public class OrderResponseItem
	{
		public string OrderItemId { get; set; } = null!;
		public string ProductId { get; set; } = null!;
		public string ProductName { get; set; } = null!;
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }
	}
}
