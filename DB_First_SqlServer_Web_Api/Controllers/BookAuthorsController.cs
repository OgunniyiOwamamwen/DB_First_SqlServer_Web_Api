using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_First_SqlServer_Web_Api.Models;

namespace DB_First_SqlServer_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public BookAuthorsController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/BookAuthors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookAuthors>>> GetBookAuthors()
        {
            return await _context.BookAuthors.ToListAsync();
        }

        // GET: api/BookAuthors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookAuthors>> GetBookAuthors(long id)
        {
            var bookAuthors = await _context.BookAuthors.FindAsync(id);

            if (bookAuthors == null)
            {
                return NotFound();
            }

            return bookAuthors;
        }

        // PUT: api/BookAuthors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookAuthors(long id, BookAuthors bookAuthors)
        {
            if (id != bookAuthors.BookId)
            {
                return BadRequest();
            }

            _context.Entry(bookAuthors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookAuthorsExists(id))
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

        // POST: api/BookAuthors
        [HttpPost]
        public async Task<ActionResult<BookAuthors>> PostBookAuthors(BookAuthors bookAuthors)
        {
            _context.BookAuthors.Add(bookAuthors);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookAuthorsExists(bookAuthors.BookId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookAuthors", new { id = bookAuthors.BookId }, bookAuthors);
        }

        // DELETE: api/BookAuthors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookAuthors>> DeleteBookAuthors(long id)
        {
            var bookAuthors = await _context.BookAuthors.FindAsync(id);
            if (bookAuthors == null)
            {
                return NotFound();
            }

            _context.BookAuthors.Remove(bookAuthors);
            await _context.SaveChangesAsync();

            return bookAuthors;
        }

        private bool BookAuthorsExists(long id)
        {
            return _context.BookAuthors.Any(e => e.BookId == id);
        }
    }
}
