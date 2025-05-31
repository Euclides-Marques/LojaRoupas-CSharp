using CsvHelper;
using CsvHelper.Configuration;
using LojaRoupas.Context;
using LojaRoupas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Diagnostics.Contracts;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace LojaRoupas.Controllers
{
    public class MainController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MainController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        //Aqui eu estou usando uma rota para falar qual será ela e o ID do produto que eu vou poder ver o detalhe
        [HttpGet("Detalhes/{id:guid}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roupa = await _context.Roupas.Include(c => c.Categoria).Include(m => m.Marca).FirstOrDefaultAsync(r => r.Id == id);

            if (roupa == null)
            {
                return NotFound();
            }

            return View(roupa);
        }

        //Aqui eu estou usando uma rota para falar qual será ela e o ID do produto que eu vou poder ver a categoria
        [HttpGet("Categoria/{categoriaId:guid}")]
        public IActionResult GetRoupasByCategoria(Guid categoriaId)
        {
            if (categoriaId == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", new { categoriaId });
        }

        //Aqui eu estou usando uma rota para falar qual será ela e o ID do produto que eu vou poder ver a marca
        [HttpGet("Marca/{marcaId:guid}")]
        public IActionResult GetRoupasByMarca(Guid marcaId)
        {
            if (marcaId == null)
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

        //Aqui eu estou usando uma rota para falar qual será ela e o ID do produto que eu vou poder ver os favoritos
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

            if (roupa == null)
            {
                return NotFound();
            }

            roupa.AdicionarCarrinho = !roupa.AdicionarCarrinho;

            _context.Update(roupa);
            _context.SaveChanges();

            return RedirectToAction("Carrinho");
        }

        //Aqui eu estou usando uma rota para falar qual será ela e o ID do produto que eu vou poder ver o carrinho
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

        //Aqui eu estou usando uma rota para falar qual será ela e o ID do produto que eu vou poder ver os lançamentos
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

        //Aqui eu estou usando uma rota para falar qual será ela e o ID do produto que eu vou poder ver os pedidos finalizados
        [HttpGet]
        public IActionResult PedidoFinalizado()
        {
            var roupasCarrinho = _context.Roupas.Where(r => r.AdicionarCarrinho == true).ToList();

            if (roupasCarrinho.Count() < 1)
            {
                return NotFound();
            }

            foreach (var roupa in roupasCarrinho)
            {
                roupa.AdicionarCarrinho = false;
            }

            _context.SaveChanges();

            return View();
        }

        //Aqui eu estou vendo os contratos que foram importados pelo usuário que está logado no sistema
        [HttpGet]
        public IActionResult Contrato(int page = 1)
        {
            int pageSize = 5;

            var contratos = _context.Contratos
                .OrderByDescending(c => c.DataImportacao)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPaginas = Math.Ceiling((double)_context.Contratos.Count() / pageSize);
            ViewBag.PaginaAtual = page;

            return View(contratos);
        }

        //Aqui eu faço a importação dos arquivos
        [HttpPost]
        public async Task<IActionResult> ImportarContratos(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return RedirectToAction("Error", "Main");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var nomeUsuario = user.UserName;

            using var reader = new StreamReader(arquivo.OpenReadStream(), Encoding.GetEncoding("iso-8859-1"));
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using var csv = new CsvReader(reader, config);
            var registros = csv.GetRecords<dynamic>();

            foreach (var reg in registros)
            {
                _context.Contratos.Add(new Contratos
                {
                    Id = Guid.NewGuid(),
                    Nome = reg.Nome,
                    CPF = reg.CPF,
                    NumeroContrato = reg.Contrato,
                    Produto = reg.Produto,
                    Vencimento = DateTime.Parse(reg.Vencimento),
                    Valor = decimal.Parse(reg.Valor),
                    UsuarioImportador = nomeUsuario,
                    DataImportacao = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Importado com sucesso!";
            return RedirectToAction("Contrato");
        }

        //Aqui ele vai me levar a tela de detalhes do contrato
        public IActionResult DetalhesContrato(Guid id)
        {
            var contrato = _context.Contratos.FirstOrDefault(c => c.Id == id);
            if (contrato == null) return NotFound();
            return View(contrato);
        }

        //Aqui ele vai me levar a tela de detalhes do contrato
        public IActionResult ResumoCliente(Guid id)
        {
            var contrato = _context.Contratos.FirstOrDefault(c => c.Id == id);
            if (contrato == null) return NotFound();

            var contratosCliente = _context.Contratos
                .Where(c => c.CPF == contrato.CPF)
                .ToList();

            var total = contratosCliente.Sum(c => c.Valor);
            var maiorAtraso = contratosCliente
                .Select(c => (DateTime.Now - c.Vencimento).Days)
                .Max();

            ViewBag.Total = total;
            ViewBag.MaiorAtraso = maiorAtraso;
            ViewBag.Cliente = contrato.Nome;

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
