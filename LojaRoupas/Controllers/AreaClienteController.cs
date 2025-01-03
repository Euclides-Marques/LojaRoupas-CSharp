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

        [HttpPost]
        public async Task<IActionResult> Salvar(Cliente model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var cliente = await _dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == user.ClienteId);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Nome = model.Nome ?? cliente.Nome;
            cliente.CPF = model.CPF ?? cliente.CPF;
            cliente.Email = model.Email ?? cliente.Email;
            cliente.Celular = model.Celular ?? cliente.Celular;
            cliente.CEP = model.CEP ?? cliente.CEP;
            cliente.Logradouro = model.Logradouro ?? cliente.Logradouro;
            cliente.Numero = model.Numero != 0 ? model.Numero : cliente.Numero;
            cliente.Bairro = model.Bairro ?? cliente.Bairro;
            cliente.Estado = model.Estado ?? cliente.Estado;
            cliente.Cidade = model.Cidade ?? cliente.Cidade;

            _dbContext.Clientes.Update(cliente);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("MeusDados");
        }

    }
}