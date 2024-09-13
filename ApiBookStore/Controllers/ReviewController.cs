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

namespace ApiBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly DataContext _context;

        public ReviewController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Review/bookId/5
        [HttpGet("bookId/{bookId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByBookId(int bookId)
        {
            var reviews = await _context.Reviews
                .AsNoTracking()
                .Where(r => r.BookId == bookId)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    Score = r.Score,
                    Description = r.Description,
                    BookId = r.BookId,
                    UserId = r.UserId,
                    User = new UserDto
                    {
                        UserId = r.User.UserId,
                        Name = r.User.Name,
                        Surname = r.User.Surname,
                        Email = r.User.Email,
                        PhoneNumber = r.User.PhoneNumber
                    }
                })
                .ToListAsync();

            return Ok(reviews);
        }

        // GET: api/Review/userId/5
        [HttpGet("userId/{userId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByUserId(int userId)
        {
            var reviews = await _context.Reviews
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    Score = r.Score,
                    Description = r.Description,
                    BookId = r.BookId,
                    UserId = r.UserId,
                    User = new UserDto
                    {
                        UserId = r.User.UserId,
                        Name = r.User.Name,
                        Surname = r.User.Surname,
                        Email = r.User.Email,
                        PhoneNumber = r.User.PhoneNumber
                    }
                })
                .ToListAsync();

            return Ok(reviews);
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews
                .AsNoTracking()
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    Score = r.Score,
                    Description = r.Description,
                    BookId = r.BookId,
                    UserId = r.UserId,
                    User = new UserDto
                    {
                        UserId = r.User.UserId,
                        Name = r.User.Name,
                        Surname = r.User.Surname,
                        Email = r.User.Email,
                        PhoneNumber = r.User.PhoneNumber
                    }
                }).SingleOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // PUT: api/Review/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.ReviewId)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Review
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview([FromBody] Review review)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            // Verifico che l'utente loggato crei un suo commento
            if (userId != review.UserId.ToString())
            {
                return BadRequest();
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.ReviewId }, review);
        }

        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
