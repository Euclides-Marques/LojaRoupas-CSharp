using LojaRoupas.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace LojaRoupas.Controllers
{
    public class MainController : Controller
    {
        private readonly AppDbContext _context;

        public MainController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string filter, int pagindex = 1, string sort = "Nome")
        {
            var resultado = _context.Roupas.Include(r => r.Categoria).Include(r => r.Marca).Where(r => r.Ativo == true).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(r => r.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, resultado.Count(), pagindex, sort, "Nome");
            ViewData["Filter"] = filter;
            return View(model);
        }

        [HttpGet("Detalhes/{id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            if(id == null) 
            {
                return NotFound();
            }

            var roupa = await _context.Roupas.Include(c => c.Categoria).Include(m => m.Marca).FirstOrDefaultAsync(r => r.Id == id);

            if(roupa == null)
            {
                return NotFound();
            }

            return View(roupa);
        }

        public IActionResult Carrinho()
        {
            return View();
        }

        public IActionResult Favoritos()
        {
            return View();
        }
    }
}
