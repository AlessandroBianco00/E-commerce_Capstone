﻿using System;
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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetMyOrders()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var myOrders = await _orderService.GetMyOrders(userId);

            return Ok(myOrders);
        }

        // GET: api/Order/myOrder/5
        [HttpGet("myOrder/{id}")]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<OrderDto>> PostOrder([FromBody] Order order)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (userId != order.UserId.ToString())
            {
                return BadRequest();
            }

            var cart = await _cartService.GetMyCart(userId);    

            if (cart == null) return NotFound();

            if (cart.Books == null || !cart.Books.Any())
            {
                return BadRequest("Cart is empty, cannot create an order.");
            }

            order.OrderDate = DateOnly.FromDateTime(DateTime.Now);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderId = order.OrderId;

            var orderItems = new List<OrderItem>();
            var cartItemsToRemove = new List<CartItem>();

            foreach (var ci in cart.Books)
            {
                var discountPercentage = (ci.Book.Discount != null && ci.Book.Discount.EndingDate > DateOnly.FromDateTime(DateTime.Now))
                    ? ci.Book.Discount.DiscountPercentage
                    : 5;

                orderItems.Add(new OrderItem
                {
                    Quantity = ci.Quantity,
                    Price = ci.Book.Price * (100 - discountPercentage) / 100,
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

        // Delete non necessaria. Il database deve mantenere i dati contabili dei vari ordini
    }
}
