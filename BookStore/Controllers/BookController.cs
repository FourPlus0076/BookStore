using BookStore.Models;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetBook(int Id) 
        {
            var data = _bookRepository.GetBook(Id);
            return View(data);
        }
    }
}
