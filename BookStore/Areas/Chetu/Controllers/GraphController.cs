using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Chetu.Controllers
{
    [Area("Chetu")]
    public class GraphController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Graph()
        {
            return View();
        }
    }
}
