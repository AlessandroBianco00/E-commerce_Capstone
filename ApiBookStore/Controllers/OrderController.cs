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
        private readonly IOrderService _orderService;

        public OrderController(DataContext context, ICartService cartService, IOrderService orderService)
        {
            _context = context;
            _cartService = cartService;
            _orderService = orderService;
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
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetMyOrders()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var myOrders = await _orderService.GetMyOrders(userId);

            return Ok(myOrders);
        }

        // GET: api/Order/myOrder/5
        [HttpGet("myOrder/{id}")]
        public async Task<ActionResult<OrderDto>> GetMyOrder(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var myOrder = await _orderService.GetMyOrderById(id, userId);

            if (myOrder == null)
            {
                return NotFound();
            }

            return Ok(myOrder);
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder([FromBody] Order order)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userId != order.UserId.ToString())
            {
                return BadRequest();
            }

            order.OrderDate = DateOnly.FromDateTime(DateTime.Now).AddDays(3);
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

            return CreatedAtAction("GetMyOrder", new { id = order.OrderId }, new OrderDto
            {
                OrderId = order.OrderId,
            });
        }

        // PUT: api/Order/5

        // DELETE: api/Order/5

    }
}
