using BLL.Interfaces;
using BLL.Services;
using Datalayer.Context;
using Entities;
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


            //create super admin
            using var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (!userManager.Users.Any())
            {
                User user = new()
                {
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "super@admin.ka",
                    PhoneNumber = "7777777",
                    EmailConfirmed = true,
                    UserName = "superadmin"
                };

                await userManager.CreateAsync(user, "Super.Adm1n");
                await userManager.AddToRoleAsync(user, UserRoles.SuperAdmin);
            }


            var dbContext = serviceScope.ServiceProvider.GetRequiredService<HotelDbContext>();
            if (!dbContext.Orders.Any())
            {
                await dbContext.Orders.AddAsync(new Order()
                {
                    GuestId = "",
                    StartDate = "",
                    EndDate = "",
                    Additional = "",
                    BookedDate = "",
                    ConfirmedDate = "",
                    RoomId = 0,
                    RoomTypeId = 0,
                    NumberOfAdults= 0,
                    NumberOfChildren= 0,
                    TotalPrice= 0,
                    OrderStatus = OrderStatus.Unknown
                });
                dbContext.SaveChanges();
            }
        }
    }
}
