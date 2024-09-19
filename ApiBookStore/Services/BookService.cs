using ApiBookStore.Context;
using ApiBookStore.DTO;
using ApiBookStore.Entities;
using ApiBookStore.Interfaces;
using ApiBookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBookStore.Services
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        private readonly IImageService _imageService;

        public BookService(DataContext dbContext, IImageService imageService)
        {
            _context = dbContext;
            _imageService = imageService;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var books = await _context.Books
                .AsNoTracking()
                .Select(b => new Book
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Description = b.Description,
                    Image = b.Image,
                    Price = b.Price,
                    Editor = b.Editor,
                    Pages = b.Pages,
                    ISBN = b.ISBN,
                    Language = b.Language,
                    PublicationDate = b.PublicationDate,
                    QuantityAvailable = b.QuantityAvailable,
                    AuthorId = b.AuthorId,
                    TranslatorId = b.TranslatorId,
                    DiscountId = b.DiscountId,
                    Author = new Author
                    {
                        AuthorId = b.Author.AuthorId,
                        AuthorName = b.Author.AuthorName,
                        Image = b.Author.Image,
                        Description = b.Author.Description
                    },
                    Translator = new Translator
                    {
                        TranslatorId = b.Translator.TranslatorId,
                        TranslatorName = b.Translator.TranslatorName
                    },
                    Discount = b.Discount,
                    Categories = b.Categories.Select(c => new Category
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    }).ToList()
                })
                .ToListAsync();
            return books;
        }

        public async Task<Book?> GetById(int id)
        {
            var book = await _context.Books
                .AsNoTracking()
                .Select(b => new Book
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Description = b.Description,
                    Image = b.Image,
                    Price = b.Price,
                    Editor = b.Editor,
                    Pages = b.Pages,
                    ISBN = b.ISBN,
                    Language = b.Language,
                    PublicationDate = b.PublicationDate,
                    QuantityAvailable = b.QuantityAvailable,
                    AuthorId = b.AuthorId,
                    TranslatorId = b.TranslatorId,
                    DiscountId = b.DiscountId,
                    Author = new Author
                    {
                        AuthorId = b.Author.AuthorId,
                        AuthorName = b.Author.AuthorName,
                        Image = b.Author.Image,
                        Description = b.Author.Description
                    },
                    Translator = new Translator
                    {
                        TranslatorId = b.Translator.TranslatorId,
                        TranslatorName = b.Translator.TranslatorName
                    },
                    Discount = b.Discount,
                    Categories = b.Categories.Select(c => new Category
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    }).ToList(),
                    Reviews = b.Reviews.Select(r => new Review
                    {
                        ReviewId = r.ReviewId,
                        Score = r.Score,
                        Description = r.Description,
                        UserId = r.UserId,
                        BookId = r.BookId,
                        User = new User
                        {
                            Name = r.User.Name,
                            Surname = r.User.Surname,
                            Email = r.User.Email
                        }
                    }).ToList()
                }).SingleOrDefaultAsync(b => b.BookId == id);

            return book;
        }

        public async Task<Book> Create(BookModel bookModel)
        {

            var imgBase64 = _imageService.ConvertImage(bookModel.Image);

            var book = new Book
            {
                Title = bookModel.Title,
                Description = bookModel.Description,
                Image = imgBase64,
                Price = bookModel.Price,
                Editor = bookModel.Editor,
                Pages = bookModel.Pages,
                ISBN = bookModel.ISBN,
                Language = bookModel.Language,
                PublicationDate = bookModel.PublicationDate,
                QuantityAvailable = bookModel.QuantityAvailable,
                AuthorId = bookModel.AuthorId,
                TranslatorId = bookModel.TranslatorId,
                DiscountId = bookModel.DiscountId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<BookDetailDto?> GetBookDetail(int id)
        {
            var book = await _context.Books
               .AsNoTracking()
               .Select(b => new BookDetailDto
               {
                   BookId = b.BookId,
                   Title = b.Title,
                   Description = b.Description,
                   Image = b.Image,
                   Price = b.Price,
                   Editor = b.Editor,
                   Pages = b.Pages,
                   ISBN = b.ISBN,
                   Language = b.Language,
                   PublicationDate = b.PublicationDate,
                   QuantityAvailable = b.QuantityAvailable,
                   AuthorId = b.AuthorId,
                   TranslatorId = b.TranslatorId,
                   DiscountId = b.DiscountId,
                   Discount = b.Discount,
                   Author = new AuthorSearchDto
                   {
                       AuthorId = b.Author.AuthorId,
                       AuthorName = b.Author.AuthorName,
                   },
                   Translator = new TranslatorSearchDto
                   {
                       TranslatorId = b.Translator.TranslatorId,
                       TranslatorName = b.Translator.TranslatorName
                   },
                   Categories = b.Categories.Select(c => new CategoryDto
                   {
                       CategoryId = c.CategoryId,
                       CategoryName = c.CategoryName
                   }).ToList(),
                   Reviews = b.Reviews.Select(r => new ReviewDto
                   {
                       ReviewId = r.ReviewId,
                       Score = r.Score,
                       Description = r.Description,
                       BookId = r.BookId,
                       UserId = r.UserId,
                       User = new UserDto
                       {
                           UserId = r.UserId,
                           Name = r.User.Name,
                           Surname = r.User.Surname,
                           Email = r.User.Email,
                       }
                   }).ToList(),
               }).SingleOrDefaultAsync(b => b.BookId == id);

            return book;
        }

        public async Task<IEnumerable<BookSearchDto>> GetBooksByCategoryId(int categoryId)
        {
            var books = await _context.Books
                .AsNoTracking()
                .Where(b => b.Categories[0].CategoryId == categoryId)
                .Take(4)
                .Select(b => new BookSearchDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Description = b.Description,
                    Image = b.Image,
                    Price = b.Price,
                    Editor = b.Editor,
                    Language = b.Language,
                    QuantityAvailable = b.QuantityAvailable,
                    AuthorId = b.AuthorId,
                    TranslatorId = b.TranslatorId,
                    DiscountId = b.DiscountId,
                    Discount = b.Discount,
                    Author = new AuthorSearchDto
                    {
                        AuthorId = b.Author.AuthorId,
                        AuthorName = b.Author.AuthorName,
                    },
                    Translator = new TranslatorSearchDto
                    {
                        TranslatorId = b.Translator.TranslatorId,
                        TranslatorName = b.Translator.TranslatorName
                    },
                    Categories = b.Categories.Select(c => new CategoryDto
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    }).ToList(),
                }).ToListAsync();

            return books;
        }
    }
}
