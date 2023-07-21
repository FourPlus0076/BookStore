using BookStore.Models;
using BookStore.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILanguageRepository _languageRepository;
        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _languageRepository= languageRepository;
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
        public async Task<IActionResult> AddNewBook(bool isSuccess=false,int bookId=0)
        {
            var book = new BookModel() { 
              //language= "Urdu",
            };
            ViewBag.language = new SelectList(await _languageRepository.GetAllLanguage(),"Id","Name");
            //ViewBag.language = new List<string>() {"Hindi","English","Urdu" };
            //ViewBag.language = new SelectList(GetLanguages(),"Id","Name");
            //var group1 = new SelectListGroup() { Name = "Group 1" };
            //var group2 = new SelectListGroup() { Name = "Group 2" };
            //var group3 = new SelectListGroup() { Name = "Group 3" };

            //ViewBag.language = new List<SelectListItem>()
            //{
            //    new SelectListItem { Text="Hindi",Value="1", Group=group1},
            //    new SelectListItem { Text="Engish",Value="2",Group=group1},
            //    new SelectListItem { Text="Urdu",Value="3", Group = group2},
            //    new SelectListItem { Text="Chines",Value="4", Group = group2},
            //    new SelectListItem { Text="Franch",Value="5", Group = group3},
            //    new SelectListItem { Text="Urdu1",Value="6", Group = group3}
            //};
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var id = await _bookRepository.AddNewBook(model);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                    //TempData["msg"] = "Submit Successfully--";
                }
                //else { TempData["msg"] = "Error Please Try Again"; }              
               
            }
            ViewBag.language = new SelectList(await _languageRepository.GetAllLanguage(), "Id", "Name");
            ViewBag.IsSuccess = false;
            ViewBag.BookId = 0;

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

        //public async Task<List<LanguageModel>> GetLanguages()
        //{
        //    var languages = await _languageRepository.GetAllLanguage();
        //    return languages;
        //}
    }
}
