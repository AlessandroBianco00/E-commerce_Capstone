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
    public class TranslatorController : ControllerBase
    {
        private readonly DataContext _context;

        public TranslatorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Translator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Translator>>> GetTranslators()
        {
            return await _context.Translators.ToListAsync();
        }

        // GET: api/Translator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Translator>> GetTranslator(int id)
        {
            var translator = await _context.Translators.FindAsync(id);

            if (translator == null)
            {
                return NotFound();
            }

            return translator;
        }

        // PUT: api/Translator/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTranslator(int id, Translator translator)
        {
            if (id != translator.TranslatorId)
            {
                return BadRequest();
            }

            _context.Entry(translator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslatorExists(id))
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

        // POST: api/Translator
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Translator>> PostTranslator(Translator translator)
        {
            _context.Translators.Add(translator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTranslator", new { id = translator.TranslatorId }, translator);
        }

        // DELETE: api/Translator/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranslator(int id)
        {
            var translator = await _context.Translators.FindAsync(id);
            if (translator == null)
            {
                return NotFound();
            }

            _context.Translators.Remove(translator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TranslatorExists(int id)
        {
            return _context.Translators.Any(e => e.TranslatorId == id);
        }
    }
}
