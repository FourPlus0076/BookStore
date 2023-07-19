using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<BookModel>> GetAllBook()
        {
            var books = new List<BookModel>();
            var allBooks =  await _context.Books.ToListAsync();
            if (allBooks?.Any()==true)
            {
                foreach (var item in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Id= item.Id,
                        Title= item.Title,
                        Name= item.Name,
                        Description= item.Description,
                        Category= item.Category,
                        language=item.language,
                        TotalPages= item.TotalPages,
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                var bookDetails = new BookModel()
                {
                    Id= book.Id,
                    Title = book.Title,
                    Name = book.Name,
                    Description = book.Description,
                    Category = book.Category,
                    language = book.language,
                    TotalPages = book.TotalPages,
                };
                return bookDetails;
            }
            return null;

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

        public async Task<int> AddNewBook(BookModel model)
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
            await _context.Books.AddAsync(newBook);
           await _context.SaveChangesAsync();

            return newBook.Id;

        }
    }
}
