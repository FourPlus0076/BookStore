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
        public async Task<IActionResult> GetAllBook()
        {
            var data= await _bookRepository.GetAllBook();
            return View(data);
        }
        public IActionResult AddNewBook(bool isSuccess=false,int bookId=0)
        { 
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel model)
        {
            var id = await _bookRepository.AddNewBook(model);
            if (id >0)
            {
                return RedirectToAction(nameof(AddNewBook),new { isSuccess=true , bookId =id});
                //TempData["msg"] = "Submit Successfully--";
            }
            //else { TempData["msg"] = "Error Please Try Again"; }               
            return View();
        }
        [Route("Book-details/{Id}",Name ="bookDetailsRoute")]
        public async Task<IActionResult> GetBook(int Id) 
        {
            //dynamic data = new ExpandoObject();
            var book = await _bookRepository.GetBookById(Id);
            //data.Name = "Gaus";
            //var data = 
            return View(book);
        }
    }
}
