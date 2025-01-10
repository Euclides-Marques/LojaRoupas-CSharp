using LojaRoupas.Context;
using LojaRoupas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace LojaRoupas.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminRoupasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminRoupasController(AppDbContext context)
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

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome");
            ViewBag.MarcaId = new SelectList(_context.Marcas, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Tamanho,Cor,Material,Preco,Estoque,Descricao,Genero,DataDeCadastro,CategoriaId,MarcaId,Ativo")] Roupa roupa, IFormFile imagem)
        {
            if (!ModelState.IsValid)
            {
                if (Request.Form["Genero"].Count > 0)
                {
                    roupa.Genero = string.Join(";", Request.Form["Genero"]);
                }

                if (imagem != null && imagem.Length > 0)
                {
                    var pastaImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "roupas");

                    if (!Directory.Exists(pastaImagens))
                    {
                        Directory.CreateDirectory(pastaImagens);
                    }

                    var nomeArquivo = Path.GetFileName(imagem.FileName);

                    nomeArquivo = nomeArquivo.Replace(" ", "_").ToLower(); 

                    var caminho = Path.Combine(pastaImagens, nomeArquivo);

                    using (var stream = new FileStream(caminho, FileMode.Create))
                    {
                        await imagem.CopyToAsync(stream);
                    }

                    roupa.ImagemUrl = "~/images/roupas/" + nomeArquivo;
                }
                else
                {
                    roupa.ImagemUrl = "~/images/roupas/produto-sem-imagem.png";
                }

                _context.Add(roupa);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome", roupa.CategoriaId);
            ViewBag.MarcaId = new SelectList(_context.Marcas, "Id", "Nome", roupa.MarcaId);
            return View(roupa);
        }
    }
}