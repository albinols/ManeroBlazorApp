using System.ComponentModel.DataAnnotations;

namespace ManeroBlazorAppv2.Models;

public class SignUpModel
{
    [Required(ErrorMessage = "You must enter an first name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter an last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter an email")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Your email isnt formatted correctly")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "You must enter an password")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$", ErrorMessage = "Password format incorrect.")]

    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "You must confirm your password")]
    [Compare("Password", ErrorMessage = "The passwords doesn't match")]
    public string? ConfirmedPassword { get; set; } = null!;

}
