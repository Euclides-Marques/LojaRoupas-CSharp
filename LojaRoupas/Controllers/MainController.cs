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
            var resultado = _context.Roupas.Include(r => r.Categoria).Include(r => r.Marca).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(r => r.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pagindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }
    }
}
