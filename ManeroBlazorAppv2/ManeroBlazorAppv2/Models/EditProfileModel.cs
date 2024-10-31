using System.ComponentModel.DataAnnotations;

namespace ManeroBlazorAppv2.Models;

public class EditProfileModel
{
    public string UserId { get; set; } = null!;
    
    [Required(ErrorMessage = "You must enter a name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "You must enter an email")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Your email isnt formatted correctly")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a phone number")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "You must enter a location")]
    public string Location { get; set; } = null!;

    public string? ProfileImageUrl { get; set; }
}
