using Azure.Messaging.ServiceBus;
using ManeroBlazorAppv2.Components.Account.Pages.Manage;
using ManeroBlazorAppv2.Data;
using ManeroBlazorAppv2.Factories;
using ManeroBlazorAppv2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace ManeroBlazorAppv2.Services;

public class AuthService(UserManager<ApplicationUser> userManager, ILogger<AuthService> logger, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly ILogger<AuthService> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    public async Task<bool> CreateUserAsync(SignUpModel model)
    {
        try
        {
            var user = UserFactory.CreateUserEntity(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            return result.Succeeded;

        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : AuthService:CreateUserAsync() :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UserExists(string email)
    {
        try
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email);

        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : AuthService:UserExists() :: {ex.Message}");
            return false;
        }

    }

    public async Task<bool> SendVerificationRequestAsync(string email)
    {
        try
        {

            //var client = new ServiceBusClient(_configuration.GetConnectionString("ServiceBusConnection"));
            var client = new ServiceBusClient(Environment.GetEnvironmentVariable("ServiceBusConnection"));
            var sender = client.CreateSender("verification_request");
            var message = new ServiceBusMessage(JsonConvert.SerializeObject(new { Email = email })) { ContentType = "application/json" };
            await sender.SendMessageAsync(message);
            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : AuthService:SendVerificationRequestAsync() :: {ex.Message}");
            return false;
        }

    }

    public async Task<bool> ConfirmAsync(ConfirmModel model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return false;
            }

            using var http = new HttpClient();
            var respone = await http.PostAsJsonAsync("https://verificationprovider-manero.azurewebsites.net/api/validate?code=wzPjoWsYcUOgDChHs8X3txzbevuyHXDTZrKBeEW4V0bIAzFuLjjPHA%3D%3D", model);

            if (respone.IsSuccessStatusCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return true;
            }

        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : AuthService:ConfirmAsync() :: {ex.Message}");
            return false;
        }
        return false;
    }

    public async Task<bool> IsConfirmed(string email)
    {
        try
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email && x.EmailConfirmed);

        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : AuthService:IsConfirmed() :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteUser(string email)
    {
        try
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return true;

            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : Userservice:DeleteUser() :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SignInUserAsync(SignInModel model)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.isPersistent, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : Userservice:SignInUserAsync() :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SignOutUserAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : AuthService:SignOutUserAsync() :: {ex.Message}");
            return false;
        }
    }
}
