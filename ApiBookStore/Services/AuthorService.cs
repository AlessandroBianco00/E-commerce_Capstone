using ApiBookStore.Context;
using ApiBookStore.Entities;
using ApiBookStore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBookStore.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _context;

        public AuthorService(DataContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var authors = await _context.Authors
                .AsNoTracking()
                .Select(a => new Author
                {
                    AuthorId = a.AuthorId,
                    AuthorName = a.AuthorName,
                    Image = a.Image,
                    Description = a.Description,
                    Books = a.Books.Select(b => new Book
                    {
                        BookId = b.BookId,
                        Title = b.Title,
                        Description = b.Description,
                        Image = b.Image,
                        Price = b.Price,
                        Editor = b.Editor,
                        Language = b.Language
                    }).ToList()
                })
                .ToListAsync();

            return authors;
        }

        public async Task<Author?> GetById(int id)
        {
            var author = await _context.Authors
                .AsNoTracking()
                .Select(a => new Author
                {
                    AuthorId = a.AuthorId,
                    AuthorName = a.AuthorName,
                    Image = a.Image,
                    Description = a.Description,
                    Books = a.Books.Select(b => new Book
                    {
                        BookId = b.BookId,
                        Title = b.Title,
                        Description = b.Description,
                        Image = b.Image,
                        Price = b.Price,
                        Editor = b.Editor,
                        Language = b.Language
                    }).ToList()
                }).SingleOrDefaultAsync(a => a.AuthorId == id);

            return author;
        }
    }
}
