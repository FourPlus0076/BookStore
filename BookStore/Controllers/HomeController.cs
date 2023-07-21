using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
#nullable disable
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]       
        public string Title { get; set; }
        [ViewData]
        public BookModel bookModel { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CustomProperty = "Gaus This Side";
            Title = "Home Page from controller";
            //var list = "Gaus This Side";
            //ViewBag.List = list;    
            bookModel = new BookModel()
            {
                Id = 1,
                Name = "BCA",
            };
            return View();
            //return View("TempView/GausTemp.cshtml");
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs() 
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}