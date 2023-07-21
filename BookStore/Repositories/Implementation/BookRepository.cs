﻿using BookStore.Data;
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
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;

        }
    }
}
