using BookStore.Models;

namespace BookStore.Repositories
{
    public interface IBookRepository
    {
        public List<BookModel> GetAllBook();
        public BookModel GetBookById(int id);
       public int AddNewBook(BookModel model);
    }
}
