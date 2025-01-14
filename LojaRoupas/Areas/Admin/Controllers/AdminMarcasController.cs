using LojaRoupas.Context;
using LojaRoupas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaRoupas.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminMarcasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminMarcasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Marcas.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Nome, DataDeCadastro, Ativo")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(marca);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FirstOrDefaultAsync(m => m.Id == id);

            if(marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        [HttpGet]
        public async Task<IActionResult> Deletar(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FirstOrDefaultAsync(m => m.Id == id);

            if(marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmar(Guid id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);

            if( marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, [Bind("Id, Nome, Ativo")] Marca marca)
        {
            if (id != marca.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!MarcaExist(marca.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExist(Guid id)
        {
            return _context.Marcas.Any(m => m.Id == id);
        }
    }
}
