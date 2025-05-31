using LojaRoupas.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace LojaRoupas.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Aqui nessa parte ele verá se existe a role cliente ou admin, e caso não exista ele cria e salva no banco
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

        //Aqui nessa parte ele verá se existe o e-mail de adim, e caso não exista ele cria e salva no banco
        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("admin@admin").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
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
