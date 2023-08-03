using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Chetu.Controllers
{
    [Area("Chetu")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
