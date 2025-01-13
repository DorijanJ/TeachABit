using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Uloge;

namespace TeachABit.API.Seed
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Uloga>>();

            Uloga korisnik = new()
            {
                Name = "Korisnik",
                LevelPristupa = 1,
            };

            Uloga moderator = new()
            {
                Name = "Moderator",
                LevelPristupa = 2,
            };

            Uloga admin = new()
            {
                Name = "Admin",
                LevelPristupa = 3,
            };

            List<Uloga> ulogaList = [korisnik, moderator, admin];

            foreach (var uloga in ulogaList)
            {
                if (uloga.Name != null && !await roleManager.RoleExistsAsync(uloga.Name))
                {
                    await roleManager.CreateAsync(uloga);
                }
            }

        }

        public static async Task SeedUser(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Uloga>>();
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
