using Microsoft.AspNetCore.Identity;
using System.Data;

namespace LojaRoupas.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Cliente").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Cliente";
                role.NormalizedName = "CLIENTE";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("admin@admin").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Admin";
                user.Email = "admin@admin";
                user.NormalizedUserName = "ADMIN";
                user.NormalizedEmail = "ADMIN@ADMIN";
                user.EmailConfirmed = true;
                user.LockoutEnabled = true;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "R0up4T0rced0r!").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }
        }
    }
}
