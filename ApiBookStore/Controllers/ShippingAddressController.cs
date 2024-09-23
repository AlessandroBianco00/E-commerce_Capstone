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
using Microsoft.AspNetCore.Authorization;

namespace ApiBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly DataContext _context;

        public ShippingAddressController(DataContext context)
        {
            _context = context;
        }

        // CRUD che l'utente può effettuare sui propri indirizzi di spedizione

        // GET: api/ShippingAddress
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ShippingAddressDto>>> GetShippingAddresses()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var addresses = await _context.ShippingAddresses
                .AsNoTracking()
                .Where(sa => sa.UserId.ToString() == userId)
                .Select(sa => new ShippingAddressDto
                {
                    ShippingAddressId = sa.ShippingAddressId,
                    StreetAddress = sa.StreetAddress,
                    City = sa.City,
                    ZipCode = sa.ZipCode,
                    Country = sa.Country,
                    UserId = sa.UserId
                })
                .ToListAsync();

            return Ok(addresses);
        }

        // GET: api/ShippingAddress/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ShippingAddressDto>> GetShippingAddress(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var shippingAddress = await _context.ShippingAddresses
                .AsNoTracking()
                .Where(sa => sa.UserId.ToString() == userId)
                .Select(sa => new ShippingAddressDto
                {
                    ShippingAddressId = sa.ShippingAddressId,
                    StreetAddress = sa.StreetAddress,
                    City = sa.City,
                    ZipCode = sa.ZipCode,
                    Country = sa.Country,
                    UserId = sa.UserId
                })
                .SingleOrDefaultAsync(sa => sa.ShippingAddressId == id);

            if (shippingAddress == null)
            {
                return NotFound();
            }

            return Ok(shippingAddress);
        }

        // PUT: api/ShippingAddress/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutShippingAddress([FromRoute] int id, [FromBody] ShippingAddress shippingAddress)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (id != shippingAddress.ShippingAddressId || userId != shippingAddress.UserId.ToString() )
            {
                return BadRequest();
            }

            _context.Entry(shippingAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingAddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShippingAddress
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ShippingAddress>> PostShippingAddress([FromBody] ShippingAddress shippingAddress)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            // L'utente può creare solo indirizzi per il suo profilo
            if (userId != shippingAddress.UserId.ToString())
            {
                return BadRequest();
            }

            _context.ShippingAddresses.Add(shippingAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShippingAddress", new { id = shippingAddress.ShippingAddressId }, shippingAddress);
        }

        // DELETE: api/ShippingAddress/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteShippingAddress(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var shippingAddress = await _context.ShippingAddresses.FindAsync(id);
            if (shippingAddress == null)
            {
                return NotFound();
            }

            if (userId != shippingAddress.UserId.ToString())
            {
                return BadRequest();
            }

            _context.ShippingAddresses.Remove(shippingAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShippingAddressExists(int id)
        {
            return _context.ShippingAddresses.Any(e => e.ShippingAddressId == id);
        }
    }
}
