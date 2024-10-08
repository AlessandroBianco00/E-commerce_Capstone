﻿using System;
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
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Where(u => u.DeletedAt == null)
                .Include(u => u.Roles)
                .ToListAsync();

            return Ok(users);
        }

        // Get per visitare la pagina dettaglio di un altro utente

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.DeletedAt == null)
                .Include(u => u.Reviews)
                .SingleOrDefaultAsync(c => c.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // Get per visitare la propria pagina utente

        // GET: api/User/myUser/5
        [HttpGet("myUser/{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetMyUser(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.DeletedAt == null)
                .Include(u => u.Reviews)
                .SingleOrDefaultAsync(c => c.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // Patch: api/User/5
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> PatchUser(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest();
            }

            var userEntity = await _context.Users.FindAsync(id);

            if (userEntity == null)
            {
                return NotFound();
            }
            
            userEntity.Name = userDto.Name ?? userEntity.Name;
            userEntity.Surname = userDto.Surname ?? userEntity.Surname;
            userEntity.PhoneNumber = userDto.PhoneNumber ?? userEntity.PhoneNumber;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(userDto);
        }

        // Cancellazione dati dell'utente loggato
        // Non va ha cancellare i dati (utili perchè legati ad altre tabelle)
        //Il campo DeletedAt viene riempito e non sarà più visualizzabile dai non admin

        // DeleteUser: api/User/deleteUser/5
        [HttpPatch("deleteUser/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            if (id.ToString() != userId)
            {
                return BadRequest();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.DeletedAt = DateTime.Now;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.Carts.Add(new Cart { UserId = user.UserId });
            _context.Wishlists.Add(new Wishlist { UserId = user.UserId });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
