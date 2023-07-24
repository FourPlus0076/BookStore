using BookStore.Models;

namespace BookStore.Repositories.Interface
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBook();
        Task<BookModel> GetBookById(int id);
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetTopBooksAsync();
    }
}
