using Microsoft.AspNetCore.Mvc;

namespace LojaRoupas.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
