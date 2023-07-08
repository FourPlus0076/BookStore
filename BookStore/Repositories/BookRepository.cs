using BookStore.Models;

namespace BookStore.Repositories
{
    public class BookRepository
    {
        public List<BookModel> GetAllBook()
        {
            return DataSource();
        }
        public BookModel GetBook(int id)
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
    }
}
