using Microsoft.AspNetCore.Identity;

namespace API.Identity
{
    public static class AppDbInitializer
    {
        public static async Task SeedRolesToDatabase(this IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var rolemanager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await rolemanager.RoleExistsAsync(UserRoles.SuperAdmin))
            {
                await rolemanager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
            }

            if (!await rolemanager.RoleExistsAsync(UserRoles.Admin))
            {
                await rolemanager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await rolemanager.RoleExistsAsync(UserRoles.Guest))
            {
                await rolemanager.CreateAsync(new IdentityRole(UserRoles.Guest));
            }
        }
    }
}
