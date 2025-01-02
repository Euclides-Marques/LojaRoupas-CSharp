using LojaRoupas.Context;
using LojaRoupas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaRoupas.Controllers
{
    public class AreaClienteController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;

        public AreaClienteController(UserManager<ApplicationUser> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> MeusDados(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return Unauthorized();
            }

            var cliente = _dbContext.Clientes.FirstOrDefault(c => c.Id == user.ClienteId);

            if(cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }
    }
}