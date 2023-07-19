using BookStore.Data;
using BookStore.Models;

namespace BookStore.Repositories
{
#nullable disable    
    public class BookRepository: IBookRepository
    {
        private readonly BookStoreDbContext _context;
        public BookRepository(BookStoreDbContext context)
        {
            _context=context;
        }
        public List<BookModel> GetAllBook()
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){ Id=1,Title="MVC", Name="gm"},
                new BookModel(){ Id=2,Title="C#", Name="gm1"},
                new BookModel(){ Id=3, Title="Java", Name="jm"},
                new BookModel(){ Id=4,Title="html",Name="name"},
                new BookModel(){ Id=5, Title="php", Name="gaus"}
            };
        }

        public int AddNewBook(BookModel model)
        {
            var newBook = new Books()
            { 
              Title= model.Title,
              Name= model.Name,
              Description= model.Description,
              TotalPages= model.TotalPages,
              language=model.language,
              CreatedDate= DateTime.UtcNow,
            };
            _context.Books.Add(newBook);
            _context.SaveChanges();

            return newBook.Id;

        }
    }
}
