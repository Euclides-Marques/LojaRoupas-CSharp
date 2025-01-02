using Microsoft.AspNetCore.Mvc;

namespace LojaRoupas.Controllers
{
    public class AreaClienteController : Controller
    {
        public IActionResult MeusDados()
        {
            return View();
        }
    }
}