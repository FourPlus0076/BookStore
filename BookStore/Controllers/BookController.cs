using BookStore.Models;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllBook()
        {
            var data= _bookRepository.GetAllBook();
            return View(data);
        }
        public IActionResult AddNewBook()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBook(BookModel model)
        {
            var data= _bookRepository.AddNewBook(model);
            if (data !=null)
            {
                TempData["msg"] = "Submit Successfully--";
            }
            else { TempData["msg"] = "Error Please Try Again"; }
                


            return View();
        }
        [Route("Book-details/{Id}",Name ="bookDetailsRoute")]
        public IActionResult GetBook(int Id) 
        {
            dynamic data = new ExpandoObject();
            data.book = _bookRepository.GetBookById(Id);
            data.Name = "Gaus";
            //var data = 
            return View(data);
        }
    }
}
