using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.API.Seed
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = ["Admin", "Predavač", "Korisnik"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedUser(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Korisnik>>();

            if (await userManager.Users.AnyAsync(x => x.UserName == "demo")) return;

            Korisnik korisnik = new()
            {
                EmailConfirmed = true,
                UserName = "demo",
                Email = "demo@teachabit.org",
            };

            await userManager.CreateAsync(korisnik, "Password0");

            await userManager.AddToRoleAsync(korisnik, "Korisnik");
        }
    }
}
