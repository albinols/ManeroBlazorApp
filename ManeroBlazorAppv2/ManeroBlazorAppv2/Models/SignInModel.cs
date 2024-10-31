namespace ManeroBlazorAppv2.Models;

public class SignInModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool isPersistent { get; set; }
}
