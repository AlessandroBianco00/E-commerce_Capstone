using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBookStore.Context;
using ApiBookStore.Entities;
using ApiBookStore.DTO;
using ApiBookStore.Models;
using ApiBookStore.Interfaces;

namespace ApiBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;

        // Il carrello viene creato insieme alla creazione Utente è unico e non può essere cancellato o modificato. L'utente può interagirci solo aggiungendo e rimuovendo libri dal carrello.

        public CartController(DataContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            var carts = await _context.Carts
                .AsNoTracking()
                .Include(c => c.Books)
                .ThenInclude(ci => ci.Book)
                .ToListAsync();

            return Ok(carts);
        }

        // GET: api/Cart/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<Cart>> GetCart(int userId)
        {
            var cart = await _context.Carts
                .AsNoTracking()
                .Include(c => c.Books)
                .ThenInclude(ci => ci.Book)
                .SingleOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // GET: api/Cart/myCart
        [HttpGet("myCart")]
        public async Task<ActionResult<CartDto>> GetMyCart()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var cart = await _cartService.GetMyCart(userId);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // PUT: api/Cart/5

        // POST: api/Cart/addToCart
        [HttpPost("addToCart")]
        public async Task<ActionResult<CartItem>> AddToCart([FromBody] CartItem cartItem)
        {
            // Controllo se l'oggetto è già presente nel carrello
            var existingItem = _context.CartItems
                .FirstOrDefault(c => c.CartId == cartItem.CartId && c.BookId == cartItem.BookId);

            // Se presente aggiorno la quantità
            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
                _context.CartItems.Update(existingItem);
            }
            // Altrimenti lo aggiungo al carrello
            else
            {
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return Ok(cartItem);
        }

        // DELETE: api/Cart/removeFromCart/5
        [HttpDelete("removeFromCart/{cartItemId}")]
        public async Task<ActionResult<CartItem>> RemoveFromCart(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
