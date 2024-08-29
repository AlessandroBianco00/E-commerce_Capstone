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
    public class WishlistController : ControllerBase
    {
        private readonly DataContext _context;

        public WishlistController(DataContext context)
        {
            _context = context;
        }

        // La wishlist viene creata insieme alla creazione Utente è una e non può essere cancellata o modificata. L'utente può interagirci solo aggiungendo e togliendo i libri dalla wishlist

        // GET: api/Wishlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlists()
        {
            var lists = await _context.Wishlists.AsNoTracking().Include(w => w.Books).ToListAsync();
            return Ok(lists);
        }

        // GET: api/Wishlist/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<Wishlist>> GetWishlist(int userId)
        {
            var wishlist = await _context.Wishlists.AsNoTracking().Include(w => w.Books).SingleOrDefaultAsync(w => w.UserId == userId);

            if (wishlist == null)
            {
                return NotFound();
            }

            return Ok(wishlist);
        }

        // PUT: api/Wishlist/5


        // POST: api/Wishlist

        // DELETE: api/Wishlist/5

    }
}
