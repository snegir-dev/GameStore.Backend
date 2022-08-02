using GameStore.Domain;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Persistence.Initializers;

public class RoleInitializer
{
    public static async Task InitializerAsync(RoleManager<IdentityRole<long>> roleManager, 
        UserManager<User> userManager)
    {
        var roles = new[] { "Admin" };
        var defaultUserAdmin = new
        {
            UserName = "Admin",
            Email = "admina2f526a7@gmail.com",
            Password = "a2f526a7-fb93-43b4-b98d-72ea8e4fe610"
        };

        foreach (var role in roles)
        {
            var isExistRole = await roleManager.FindByNameAsync(role) != null;
            if (!isExistRole)
                await roleManager.CreateAsync(new IdentityRole<long>(role));
        }

        var userAdmin = await userManager.FindByEmailAsync(defaultUserAdmin.Email);
        if (userAdmin == null)
        {
            var user = new User()
            {
                UserName = defaultUserAdmin.UserName,
                Email = defaultUserAdmin.Email
            };
            await userManager.CreateAsync(user, defaultUserAdmin.Password);
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}