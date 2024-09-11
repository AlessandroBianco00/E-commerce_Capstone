using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBookStore.Context;
using ApiBookStore.Entities;
using ApiBookStore.Interfaces;
using ApiBookStore.DTO;

namespace ApiBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;

        public OrderController(DataContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Books)
                .ThenInclude(oi => oi.Book)
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Books)
                .ThenInclude(oi => oi.Book)
                .SingleOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET: api/Order/MyOrders
        [HttpGet("MyOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetMyOrders()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var myOrders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.UserId.ToString() == userId)
                .Include(o => o.Books)
                .ThenInclude(oi => oi.Book)
                .ToListAsync();

            return Ok(myOrders);
        }

        // GET: api/Order/myOrder/5
        [HttpGet("myOrder/{id}")]
        public async Task<ActionResult<Order>> GetMyOrder(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var myOrder = await _context.Orders
                .AsNoTracking()
                .Select(o => new OrderDto
                {
                    OrderId = id,
                    UserId = o.UserId,
                    ShippingAddressId = o.ShippingAddressId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
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
                .SingleOrDefaultAsync(o => o.OrderId == id && o.UserId.ToString() == userId);

            if (myOrder == null)
            {
                return NotFound();
            }

            return Ok(myOrder);
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] Order order)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userId != order.UserId.ToString())
            {
                return BadRequest();
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderId = order.OrderId;
            var cart = await _cartService.GetMyCart(userId);

            if (cart == null) return NotFound();

            var orderItems = new List<OrderItem>();
            var cartItemsToRemove = new List<CartItem>();

            foreach (var ci in cart.Books)
            {
                orderItems.Add(new OrderItem
                {
                    Quantity = ci.Quantity,
                    Price = ci.Book.Price,
                    OrderId = orderId,
                    BookId = ci.BookId
                });

                cartItemsToRemove.Add(await _context.CartItems.FindAsync(ci.CartItemId));
            }

            _context.OrderItems.AddRange(orderItems);
            _context.CartItems.RemoveRange(cartItemsToRemove);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // PUT: api/Order/5

        // DELETE: api/Order/5

    }
}
