using ManeroBlazorAppv2.Data;
using ManeroBlazorAppv2.Models;

namespace ManeroBlazorAppv2.Factories;

public class UserFactory
{
    public static ApplicationUser CreateUserEntity(SignUpModel model)
    {
        var entity = new ApplicationUser
        {
            Email = model.Email,
            Firstname = model.FirstName,
            Lastname = model.LastName,
            UserName = model.Email
        };
        return entity;
    }

    public static SignUpModel CreateUserModel(ApplicationUser entity)
    {
        if (entity != null)
        {
            var model = new SignUpModel
            {
                Email = entity.Email,
                Password = entity.PasswordHash,
                FirstName = entity.Firstname,
                LastName = entity.Lastname
            };
            return model;
        }
        return null!;
    }
}
