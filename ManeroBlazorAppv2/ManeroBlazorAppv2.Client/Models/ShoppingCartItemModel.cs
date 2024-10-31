using System.ComponentModel.DataAnnotations;

namespace ManeroBlazorAppv2.Client.Models
{
	public class ShoppingCartItemModel
	{
		[Required]
		public string CustomerId { get; set; } = null!;

		[Required]
		public string ProductId { get; set; } = null!;

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity must 1 or more.")]
		public int Quantity { get; set; }

		[Required(ErrorMessage = "Please select a size.")]
		public string Size { get; set; } = null!;

		[Required(ErrorMessage = "Please select a color.")]
		public string Color { get; set; } = null!;
	}
}