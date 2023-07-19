using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
#nullable disable
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) 
        {

        }
        public DbSet<Books> Books { get; set; }
    }
}
