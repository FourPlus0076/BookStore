using BookStore.Models;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;
        public BookController()
        {
            _bookRepository = new BookRepository();
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
            return View();
        }
        [Route("Book-details/{Id}",Name ="bookDetailsRoute")]
        public IActionResult GetBook(int Id) 
        {
            dynamic data = new ExpandoObject();
            data.book= _bookRepository.GetBook(Id);
            data.Name = "Gaus";
            //var data = 
            return View(data);
        }
    }
}
