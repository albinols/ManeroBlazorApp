namespace ManeroBlazorAppv2.Models
{
    public class PaymentModel
    {
        public string Id { get; set; } = null!;
        public string PaymentCompany { get; set; } = null!;
        public string CardHolderName { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public string ExpiryDate { get; set; } = null!;
        public string CVV { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
