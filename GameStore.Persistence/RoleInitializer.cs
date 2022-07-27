using Microsoft.AspNetCore.Identity;

namespace GameStore.Persistence;

public class RoleInitializer
{
    public static async Task InitializerAsync(RoleManager<IdentityRole<long>> roleManager)
    {
        var roles = new[] { "Admin" };

        foreach (var role in roles)
        {
            var isExistRole = await roleManager.FindByNameAsync(role) != null;
            if (!isExistRole)
                await roleManager.CreateAsync(new IdentityRole<long>(role));
        }
    }
}