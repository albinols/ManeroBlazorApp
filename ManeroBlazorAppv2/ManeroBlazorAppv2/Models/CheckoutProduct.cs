namespace ManeroBlazorAppv2.Models
{
    public class CheckoutProduct
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
