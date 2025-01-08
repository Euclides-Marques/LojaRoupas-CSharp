using LojaRoupas.Context;
using LojaRoupas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaRoupas.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCategoriasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminCategoriasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Nome, DataDeCadastro, Ativo")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        [HttpGet]
        public  async Task<IActionResult> Detalhes(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                                            .FirstOrDefaultAsync(c => c.Id == id);

            if(categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Deletar(Guid id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                                            .FirstOrDefaultAsync(c => c.Id == id);

            if(categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmar(Guid id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoria);
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

            var categorias = await _context.Categorias.FindAsync(id);

            if(categorias == null)
            {
                return NotFound();
            }

            return View(categorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, [Bind("Id, Nome, DataDeCadastro, Ativo")] Categoria categoria)
        {
            if(id != categoria.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExist(categoria.Id)){
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        private bool CategoriaExist(Guid id)
        {
            return _context.Categorias.Any(c => c.Id == id);
        }
    }
}
