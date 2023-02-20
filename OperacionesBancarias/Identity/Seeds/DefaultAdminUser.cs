using Application.Enums;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default Admin user
            var defaultuser = new ApplicationUser
            {
                UserName = "userAdmin",
                Email = "userAdmin@gmail.com",
                Nombre = "Juan",
                Apellido = "Muñoz",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultuser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultuser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultuser, "Juanda-123456789");
                    await userManager.AddToRoleAsync(defaultuser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultuser, Roles.Basic.ToString());

                }
            }
        }
    }
}
