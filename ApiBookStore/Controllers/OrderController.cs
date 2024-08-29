using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBookStore.Context;
using ApiBookStore.Models.Entities;

namespace ApiBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
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
                .Where(o => o.UserId
                .ToString() == userId)
                .Include(o => o.Books)
                .ThenInclude(oi => oi.Book)
                .SingleOrDefaultAsync(o => o.OrderId == id);

            if (myOrder == null)
            {
                return NotFound();
            }

            return Ok(myOrder);
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // PUT: api/Order/5

        // DELETE: api/Order/5

    }
}
