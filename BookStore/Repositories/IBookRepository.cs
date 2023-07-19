using BookStore.Models;

namespace BookStore.Repositories
{
    public interface IBookRepository
    {
       Task<List<BookModel>> GetAllBook();
       Task<BookModel> GetBookById(int id);
       Task<int> AddNewBook(BookModel model);
    }
}
