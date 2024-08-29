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
    public class ShippingAddressController : ControllerBase
    {
        private readonly DataContext _context;

        public ShippingAddressController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ShippingAddress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingAddress>>> GetShippingAddresses()
        {
            var addresses = await _context.ShippingAddresses.ToListAsync();
            return Ok(addresses);
        }

        // GET: api/ShippingAddress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingAddress>> GetShippingAddress(int id)
        {
            var shippingAddress = await _context.ShippingAddresses.FindAsync(id);

            if (shippingAddress == null)
            {
                return NotFound();
            }

            return Ok(shippingAddress);
        }

        // PUT: api/ShippingAddress/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippingAddress(int id, ShippingAddress shippingAddress)
        {
            if (id != shippingAddress.ShippingAddressId)
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShippingAddress>> PostShippingAddress(ShippingAddress shippingAddress)
        {
            _context.ShippingAddresses.Add(shippingAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShippingAddress", new { id = shippingAddress.ShippingAddressId }, shippingAddress);
        }

        // DELETE: api/ShippingAddress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingAddress(int id)
        {
            var shippingAddress = await _context.ShippingAddresses.FindAsync(id);
            if (shippingAddress == null)
            {
                return NotFound();
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
