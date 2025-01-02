﻿using LojaRoupas.Context;
using LojaRoupas.Models;
using LojaRoupas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaRoupas.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _dbContext;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (string.IsNullOrEmpty(loginVM.Email) || string.IsNullOrEmpty(loginVM.Senha))
            {
                return RedirectToAction("Error", "Account");
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Senha, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    return RedirectToAction("Error", "Account");
                }
            }
            return RedirectToAction("Error", "Account");
        }



        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                cliente.Nome = registroVM.Nome;
                cliente.Email = registroVM.Email;
                cliente.CPF = registroVM.CPF;  
                cliente.Celular = registroVM.Celular;
                cliente.CEP = registroVM.CEP;
                cliente.Logradouro = registroVM.Logradouro;
                cliente.Numero = registroVM.Numero;
                cliente.Bairro = registroVM.Bairro;
                cliente.Estado = registroVM.Estado;
                cliente.Cidade = registroVM.Cidade;
                cliente.DataInclusao = DateTime.UtcNow.AddHours(-3);

                _dbContext.Clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();

                var name = new ApplicationUser
                {
                    UserName = registroVM.Nome.ToLower(),
                    NormalizedUserName = registroVM.Nome.ToUpper(),
                    Email = registroVM.Email.ToLower(),
                    NormalizedEmail = registroVM.Email.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ClienteId = cliente.Id
                };

                var result = await _userManager.CreateAsync(name, registroVM.Senha);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(name, "Cliente");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao criar registro!");
                }
            }
            return View(registroVM);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}