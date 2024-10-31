using System.ComponentModel.DataAnnotations;

namespace ManeroBlazorApp.Models;

public class ProductUploadModel
{
    [Required(ErrorMessage = "* Product name required")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "Price required")]
    [Range(1, double.MaxValue, ErrorMessage = "* Price must be greater than 0")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "* Description required")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "* Category required")]
    public string Category { get; set; } = null!;
    
    [Required(ErrorMessage = "* SubCategory required")]
    public string SubCategory { get; set; } = null!;
    
    [Required(ErrorMessage = "*  Product type required")]
    public string ProductType { get; set; } = null!;
    
    [Required(ErrorMessage = "* Colors required")]
    public List<string> Colors { get; set; } = new List<string>();
    
    [Required(ErrorMessage = "* Tags required")]
    public List<string> Tags { get; set; } = new List<string>();
    
    [Required(ErrorMessage = "* Sizes required")]
    public List<string> Sizes { get; set; } = new List<string>();
    
    public Dictionary<string, List<string>>? ColorImageUrls { get; set; } = new Dictionary<string, List<string>>();
}
