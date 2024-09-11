using ApiBookStore.Context;
using ApiBookStore.DTO;
using ApiBookStore.Entities;
using ApiBookStore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBookStore.Services
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }
        public async Task<CartDto?> GetMyCart(string userId)
        {

            var cart = await _context.Carts
                .AsNoTracking()
                .Select(c => new CartDto
                {
                    CartId = c.CartId,
                    UserId = c.UserId,
                    Books = c.Books.Select(ci => new CartItemDto
                    {
                        CartItemId = ci.CartItemId,
                        Quantity = ci.Quantity,
                        CartId = ci.CartId,
                        BookId = ci.BookId,
                        Book = new BookSearchDto
                        {
                            BookId = ci.Book.BookId,
                            Title = ci.Book.Title,
                            Description = ci.Book.Description,
                            Image = ci.Book.Image,
                            Price = ci.Book.Price,
                            Editor = ci.Book.Editor,
                            Language = ci.Book.Language,
                            QuantityAvailable = ci.Book.QuantityAvailable,
                            AuthorId = ci.Book.AuthorId,
                            TranslatorId = ci.Book.TranslatorId,
                            DiscountId = ci.Book.DiscountId,
                            Discount = ci.Book.Discount,
                            Author = new AuthorSearchDto
                            {
                                AuthorId = ci.Book.Author.AuthorId,
                                AuthorName = ci.Book.Author.AuthorName,
                            },
                            Translator = new TranslatorSearchDto
                            {
                                TranslatorId = ci.Book.Translator.TranslatorId,
                                TranslatorName = ci.Book.Translator.TranslatorName
                            },
                            Categories = ci.Book.Categories.Select(c => new CategoryDto
                            {
                                CategoryId = c.CategoryId,
                                CategoryName = c.CategoryName
                            }).ToList()
                        }

                    }).ToList()
                })
                .SingleOrDefaultAsync(w => w.UserId.ToString() == userId);

            return cart;
        }
    }
}
