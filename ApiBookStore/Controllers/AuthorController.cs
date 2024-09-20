using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBookStore.Context;
using Microsoft.AspNetCore.Authorization;
using ApiBookStore.Interfaces;
using ApiBookStore.Models;
using ApiBookStore.Entities;

namespace ApiBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthorService _authorService;
        private readonly IImageService _imageService;

        public AuthorController(DataContext context, IImageService imageService, IAuthorService authorService)
        {
            _context = context;
            _imageService = imageService;
            _authorService = authorService;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _authorService.GetAll();

            return Ok(authors);
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor([FromRoute] int id)
        {
            var author = await _authorService.GetById(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor([FromRoute] int id, [FromForm] AuthorModel authorModel)
        {
            var imgBase64 = _imageService.ConvertImage(authorModel.Image);

            var author = new Author
            {
                AuthorId = id,
                AuthorName = authorModel.AuthorName,
                Image = imgBase64,
                Description = authorModel.Description
            };

            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor([FromForm] AuthorModel authorModel)
        {
            var imgBase64 = _imageService.ConvertImage(authorModel.Image);

            var author = new Author
            {
                AuthorName = authorModel.AuthorName,
                Image = imgBase64,
                Description = authorModel.Description
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.AuthorId }, author);
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
