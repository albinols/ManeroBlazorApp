namespace ManeroBlazorAppv2.Client.Models;

public class Product
{
	public string? Id { get; set; }
	public string? Name { get; set; }
	public decimal Price { get; set; }
	public string? Description { get; set; }
	public string? Category { get; set; }
	public string? SubCategory { get; set; }
	public string? ProductType { get; set; }
	public double? Rating { get; set; }
	public List<string>? Colors { get; set; }
	public List<string>? Tags { get; set; }
	public List<string>? Sizes { get; set; }
	public Dictionary<string, List<string>>? ColorImageUrls { get; set; }
}
