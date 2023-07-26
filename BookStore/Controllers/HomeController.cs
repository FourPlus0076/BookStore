using BookStore.Models;
using BookStore.Repositories.Interface;
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
        private readonly AlertBookModel _options;
        private readonly AlertBookModel _thirdPartyBook;
        private readonly IMessageRepository _messageRepository;
        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]       
        public string Title { get; set; }
        [ViewData]
        public BookModel bookModel { get; set; }

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IOptionsSnapshot<AlertBookModel> options, IOptionsSnapshot<AlertBookModel> thirdPartyBook,IMessageRepository messageRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _options= options.Get("InternalBook");
            _thirdPartyBook = thirdPartyBook.Get("ThirdPartyBook");
            _messageRepository = messageRepository;

        }

        public IActionResult Index()
        {
            var value = _messageRepository.GetName();
            bool isActive = _options.IsActive;
            var book = _thirdPartyBook.Name;
            //var newBook = new AlertBookModel();
            //_configuration.Bind("BookAlert", newBook);

            //var getSection = _configuration.GetSection("BookAlert");
            //var result = getSection.GetValue<bool>("GetValueTrue");
            //var book = getSection.GetValue<string>("Name");

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