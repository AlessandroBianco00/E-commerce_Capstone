using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBookStore.Context;
using ApiBookStore.Entities;

namespace ApiBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DataContext _context;

        // Il carrello viene creato insieme alla creazione Utente è unico e non può essere cancellato o modificato. L'utente può interagirci solo aggiungendo e rimuovendo libri dal carrello.

        public CartController(DataContext context)
        {
            _context = context;
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
        public async Task<ActionResult<Cart>> GetMyWishlist()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var cart = await _context.Carts
                .AsNoTracking()
                .Include(c => c.Books)
                .ThenInclude(ci => ci.Book)
                .SingleOrDefaultAsync(w => w.UserId.ToString() == userId);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // PUT: api/Cart/5

        // POST: api/Cart

        // DELETE: api/Cart/5

    }
}
