using BookStore.Data;
using BookStore.Models;
using BookStore.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementation
{
#nullable disable    
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _context;
        public BookRepository(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<BookModel>> GetAllBook()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.Books.ToListAsync();
            if (allBooks?.Any() == true)
            {
                foreach (var item in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Name = item.Name,
                        Description = item.Description,
                        Category = item.Category,
                        languageId=item.languageId,
                        //language = item.language.Name,
                        TotalPages = item.TotalPages,
                        CoverImageURL= item.CoverImageURL,
                        BookPDFURL=item.BookpdfURL
                    });
                }
            }
            return books;
        }

        public async Task<List<BookModel>> GetTopBooksAsync()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.Books.Take(5).ToListAsync();
            if (allBooks?.Any() == true)
            {
                foreach (var item in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Name = item.Name,
                        Description = item.Description,
                        Category = item.Category,
                        languageId = item.languageId,
                        //language = item.language.Name,
                        TotalPages = item.TotalPages,
                        CoverImageURL = item.CoverImageURL,
                        BookPDFURL = item.BookpdfURL
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x=>x.Id==id).Select(book => new BookModel() {
                Id= book.Id,
                Title = book.Title,
                Name = book.Name,
                Description = book.Description,
                Category = book.Category,
                languageId=book.languageId, 
                language = book.language.Name,
                TotalPages = book.TotalPages,
                CoverImageURL= book.CoverImageURL,
                Gallery=book.BookGallery.Select(x => new GalleryModel() { 
                  Id= x.Id,
                  Name= x.Name,
                  URL= x.URL,
                }).ToList(),
                BookPDFURL=book.BookpdfURL
            }).FirstOrDefaultAsync();
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Title = model.Title,
                Name = model.Name,
                Description = model.Description,
                TotalPages = model.TotalPages,
                languageId = model.languageId,
                CreatedDate = DateTime.UtcNow,
                Category= model.Category,
                CoverImageURL= model.CoverImageURL,
                BookpdfURL=model.BookPDFURL,
            };
            newBook.BookGallery=new List<BookGallery>();
            foreach (var item in model.Gallery)
            {
                newBook.BookGallery.Add(new BookGallery()
                {
                    Name= item.Name,
                    URL= item.URL,
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;

        }
    }
}
