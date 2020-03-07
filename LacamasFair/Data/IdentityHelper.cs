using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Data
{
    public static class IdentityHelper
    {
        internal static readonly string Administrator = "Administrator";

        /// <summary>
        /// Creates roles for IdentityRole
        /// </summary>
        /// <param name="provider">Service Provider</param>
        /// <param name="roles">IdentityRole roles</param>
        internal static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager =
                provider.GetRequiredService<RoleManager<IdentityRole>>();
            IdentityResult roleResult;

            foreach (string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }


        /// <summary>
        /// Seeds a new user when Update-Database is ran
        /// </summary>
        /// <param name="provider">Service Provider</param>
        /// <param name="role">Role to seed</param>
        internal static async Task SeedUsers(IServiceProvider provider, string role)
        {
            // When the owner of LacamasFair gets the website we will change this to his/her credentials
            string email = "lacamasdeveloper@lacamasfair.org";
            string password = "Developer2020";

            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            if (userManager.Users.Count() == 0)
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = email,
                    UserName = email
                };

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, role);
            }
        }

        internal static void SetIdentityConfigOptions(IdentityOptions options)
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        }
    }
}
