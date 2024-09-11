using ApiBookStore.Context;
using ApiBookStore.DTO;
using ApiBookStore.Entities;
using ApiBookStore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace ApiBookStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }
        public async Task<OrderDto?> GetMyOrderById(int orderId, string userId)
        {
            var myOrder = await _context.Orders
                .AsNoTracking()
                .Select(o => new OrderDto
                {
                    OrderId = orderId,
                    UserId = o.UserId,
                    ShippingAddressId = o.ShippingAddressId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    ShippingAddress = new ShippingAddressDto
                    {
                        ShippingAddressId = o.ShippingAddress.ShippingAddressId,
                        StreetAddress = o.ShippingAddress.StreetAddress,
                        City = o.ShippingAddress.City,
                        ZipCode = o.ShippingAddress.ZipCode,
                        Country = o.ShippingAddress.Country,
                        UserId = o.ShippingAddress.UserId
                    },
                    Books = o.Books.Select(oi => new OrderItemDto
                    {
                        OrderItemId = oi.OrderItemId,
                        Quantity = oi.Quantity,
                        Price = oi.Price,
                        OrderId = oi.OrderId,
                        BookId = oi.BookId,
                        Book = new BookSearchDto
                        {
                            BookId = oi.Book.BookId,
                            Title = oi.Book.Title,
                            Description = oi.Book.Description,
                            Image = oi.Book.Image,
                            Price = oi.Book.Price,
                            Editor = oi.Book.Editor,
                            Language = oi.Book.Language,
                            QuantityAvailable = oi.Book.QuantityAvailable,
                            AuthorId = oi.Book.AuthorId,
                            TranslatorId = oi.Book.TranslatorId,
                            DiscountId = oi.Book.DiscountId,
                            Discount = oi.Book.Discount,
                            Author = new AuthorSearchDto
                            {
                                AuthorId = oi.Book.Author.AuthorId,
                                AuthorName = oi.Book.Author.AuthorName,
                            },
                            Translator = new TranslatorSearchDto
                            {
                                TranslatorId = oi.Book.Translator.TranslatorId,
                                TranslatorName = oi.Book.Translator.TranslatorName
                            },
                            Categories = oi.Book.Categories.Select(c => new CategoryDto
                            {
                                CategoryId = c.CategoryId,
                                CategoryName = c.CategoryName
                            }).ToList()
                        }
                    }).ToList()
                })
                .SingleOrDefaultAsync(o => o.OrderId == orderId && o.UserId.ToString() == userId);

            return myOrder;
        }

        public async Task<List<OrderDto>> GetMyOrders(string userId)
        {
            var myOrders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.UserId.ToString() == userId)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    ShippingAddressId = o.ShippingAddressId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    ShippingAddress = new ShippingAddressDto
                    {
                        ShippingAddressId = o.ShippingAddress.ShippingAddressId,
                        StreetAddress = o.ShippingAddress.StreetAddress,
                        City = o.ShippingAddress.City,
                        ZipCode = o.ShippingAddress.ZipCode,
                        Country = o.ShippingAddress.Country,
                        UserId = o.ShippingAddress.UserId
                    },
                    Books = o.Books.Select(oi => new OrderItemDto
                    {
                        OrderItemId = oi.OrderItemId,
                        Quantity = oi.Quantity,
                        Price = oi.Price,
                        OrderId = oi.OrderId,
                        BookId = oi.BookId,
                        Book = new BookSearchDto
                        {
                            BookId = oi.Book.BookId,
                            Title = oi.Book.Title,
                            Description = oi.Book.Description,
                            Image = oi.Book.Image,
                            Price = oi.Book.Price,
                            Editor = oi.Book.Editor,
                            Language = oi.Book.Language,
                            QuantityAvailable = oi.Book.QuantityAvailable,
                            AuthorId = oi.Book.AuthorId,
                            TranslatorId = oi.Book.TranslatorId,
                            DiscountId = oi.Book.DiscountId,
                            Discount = oi.Book.Discount,
                            Author = new AuthorSearchDto
                            {
                                AuthorId = oi.Book.Author.AuthorId,
                                AuthorName = oi.Book.Author.AuthorName,
                            },
                            Translator = new TranslatorSearchDto
                            {
                                TranslatorId = oi.Book.Translator.TranslatorId,
                                TranslatorName = oi.Book.Translator.TranslatorName
                            },
                            Categories = oi.Book.Categories.Select(c => new CategoryDto
                            {
                                CategoryId = c.CategoryId,
                                CategoryName = c.CategoryName
                            }).ToList()
                        }
                    }).ToList()
                })
                .ToListAsync();

            return myOrders;
        }
    }
}
