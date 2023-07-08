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
        public List<BookModel> GetAllBook()
        { 
          return _bookRepository.GetAllBook();
        }
        public BookModel GetBook(int Id) 
        {
          return _bookRepository.GetBook(Id);
        }
    }
}
