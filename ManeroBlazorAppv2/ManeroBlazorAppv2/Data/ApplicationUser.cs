using Microsoft.AspNetCore.Identity;

namespace ManeroBlazorAppv2.Data;

public class ApplicationUser : IdentityUser
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string? Location { get; set; }
    public string? ProfileImageUrl { get; set; }
}
