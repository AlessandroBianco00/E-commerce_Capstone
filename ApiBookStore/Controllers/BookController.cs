using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBookStore.Context;
using ApiBookStore.Models;
using ApiBookStore.Interfaces;
using static System.Reflection.Metadata.BlobBuilder;
using ApiBookStore.Entities;
using ApiBookStore.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;

namespace ApiBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBookService _bookService;

        public BookController(DataContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookService.GetAll();

            return Ok(books);
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBook(int id, [FromForm] Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Book
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> PostBook([FromForm] BookModel bookModel)
        {
            var book = await _bookService.Create(bookModel);

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }

        // GET ricerca libri
        [HttpGet("search")]
        public async Task<ActionResult<SearchDto>> GetBooks(
            [FromQuery] string? category,
            [FromQuery] string? author,
            [FromQuery] string? title,
            [FromQuery] string? editor,
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 6) 
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(b => b.Categories.Any(c => c.CategoryName == category));
            }
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.AuthorName.Contains(author));
            }
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }
            if (!string.IsNullOrEmpty(editor))
            {
                query = query.Where(b => b.Editor.Contains(editor));
            }

            var totalBooks = await query.CountAsync();

            var books = await query
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .AsNoTracking()
               .Select(b => new BookSearchDto
               {
                   BookId = b.BookId,
                   Title = b.Title,
                   Description = b.Description,
                   Image = b.Image,
                   Price = b.Price,
                   Editor = b.Editor,
                   Language = b.Language,
                   QuantityAvailable = b.QuantityAvailable,
                   AuthorId = b.AuthorId,
                   TranslatorId = b.TranslatorId,
                   DiscountId= b.DiscountId,
                   Discount = b.Discount,
                   Author = new AuthorSearchDto
                   {
                       AuthorId = b.Author.AuthorId,
                       AuthorName = b.Author.AuthorName,
                   },
                   Translator = new TranslatorSearchDto
                   {
                       TranslatorId = b.Translator.TranslatorId,
                       TranslatorName = b.Translator.TranslatorName
                   },
                   Categories = b.Categories.Select(c => new CategoryDto
                   {
                       CategoryId = c.CategoryId,
                       CategoryName = c.CategoryName
                   }).ToList(),
               }).ToListAsync();

            var search = new SearchDto { Pages = (int)Math.Ceiling((double)totalBooks / pageSize), Books = books };

            return Ok(search);
        }

        // GET Dettaglio libro
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<Book>> GetBookDetail(int id)
        {
            var book = await _bookService.GetBookDetail(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // GET Dettaglio libro
        [HttpGet("recommended")]
        public async Task<ActionResult<IEnumerable<BookSearchDto>>> GetRecommendedBooks()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var lastCategoryId = await _context.Orders
                .Where(o => o.UserId.ToString() == userId)  // or other condition
                .OrderByDescending(o => o.OrderDate)  // get the most recent order
                .SelectMany(o => o.Books)
                .Select(oi => oi.Book.Categories.Select(c => c.CategoryId).FirstOrDefault())  // get first category ID
                .FirstOrDefaultAsync();

            var books = await _bookService.GetBooksByCategoryId(lastCategoryId);
            
            // Restituisce una lista vuota se non si è loggati (o se l'id utente non coincide)
            return Ok(books);
        }
    }
}
