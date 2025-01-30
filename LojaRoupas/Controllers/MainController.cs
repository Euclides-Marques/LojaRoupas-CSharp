using LojaRoupas.Context;
using LojaRoupas.Models;
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

        public async Task<IActionResult> Index(string filter, Guid? categoriaId, Guid? marcaId, int pagindex = 1, string sort = "Nome")
        {
            var resultado = _context.Roupas.Include(r => r.Categoria).Include(r => r.Marca).Where(r => r.Ativo == true).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(r => r.Nome.Contains(filter));
            }

            if (categoriaId.HasValue)
            {
                resultado = resultado.Where(r => r.CategoriaId == categoriaId);
            }

            if (marcaId.HasValue)
            {
                resultado = resultado.Where(r => r.MarcaId == marcaId);
            }

            var model = await PagingList.CreateAsync(resultado, resultado.Count(), pagindex, sort, "Nome");
            ViewData["Filter"] = filter;
            ViewData["CategoriaId"] = categoriaId;
            ViewData["MarcaId"] = marcaId;
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

        [HttpGet("Categoria/{categoriaId:guid}")]
        public IActionResult GetRoupasByCategoria(Guid categoriaId)
        {
            if (categoriaId == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", new { categoriaId });
        }

        [HttpGet("Marca/{marcaId:guid}")]
        public IActionResult GetRoupasByMarca(Guid marcaId)
        {
            if(marcaId == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", new { marcaId });
        }

        [HttpGet]
        public IActionResult Favoritar(Guid id)
        {
            var roupa = _context.Roupas.Find(id);

            if (roupa == null)
            {
                return NotFound();
            }

            roupa.Favorito = !roupa.Favorito;

            _context.Update(roupa);
            _context.SaveChanges();

            

            return RedirectToAction("Favoritos");
        }

        [HttpGet]
        public IActionResult Favoritos()
        {
            var roupasFav = _context.Roupas.Include(c => c.Categoria).Include(m => m.Marca).Where(r => r.Favorito == true);

            return View(roupasFav);
        }

        [HttpGet]
        public IActionResult AddCarrinho(Guid id)
        {
            var roupa = _context.Roupas.Find(id);

            if(roupa == null)
            {
                return NotFound();
            }

            roupa.AdicionarCarrinho = !roupa.AdicionarCarrinho;

            _context.Update(roupa);
            _context.SaveChanges();

            return RedirectToAction("Carrinho");
        }

        [HttpGet]
        public IActionResult Carrinho()
        {
            var roupaCarrinhno = _context.Roupas.Include(c => c.Categoria).Include(m => m.Marca).Where(rc => rc.AdicionarCarrinho == true);

            return View(roupaCarrinhno);
        }

        [HttpGet]
        public JsonResult GetCarrinhoCount()
        {
            var count = _context.Roupas.Count(rc => rc.AdicionarCarrinho == true);
            return Json(new { count });
        }

        [HttpGet]
        public IActionResult Contato()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Lancamentos()
        {
            var roupasLanc = _context.Roupas.Include(c => c.Categoria).Include(m => m.Marca).Where(r => r.Lancamento == true);

            return View(roupasLanc);
        }

        [HttpGet]
        public IActionResult Promocoes()
        {
            var roupasLanc = _context.Roupas.Include(c => c.Categoria).Include(m => m.Marca).Where(r => r.Promocao == true);

            return View(roupasLanc);
        }
    }
}
