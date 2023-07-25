using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace BookStore.Controllers
{
#nullable disable
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AlertBookModel _option;
        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]       
        public string Title { get; set; }
        [ViewData]
        public BookModel bookModel { get; set; }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IOptionsSnapshot<AlertBookModel> option)
        {
            _logger = logger;
            _configuration = configuration;
            _option = option.Value;
        }

        public IActionResult Index()
        {
            var result = _option.IsActive;

            //var book = _configuration.GetSection("AlertBook");
            //var book1 = _configuration.GetValue<bool>("AlertBook");
            //var book2 = _configuration.GetValue<string>("AlertBook");

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