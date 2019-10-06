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
    public class AuthorContactsController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public AuthorContactsController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/AuthorContacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorContact>>> GetAuthorContact()
        {
            return await _context.AuthorContact.ToListAsync();
        }

        // GET: api/AuthorContacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorContact>> GetAuthorContact(long id)
        {
            var authorContact = await _context.AuthorContact.FindAsync(id);

            if (authorContact == null)
            {
                return NotFound();
            }

            return authorContact;
        }

        // PUT: api/AuthorContacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthorContact(long id, AuthorContact authorContact)
        {
            if (id != authorContact.AuthorId)
            {
                return BadRequest();
            }

            _context.Entry(authorContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorContactExists(id))
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

        // POST: api/AuthorContacts
        [HttpPost]
        public async Task<ActionResult<AuthorContact>> PostAuthorContact(AuthorContact authorContact)
        {
            _context.AuthorContact.Add(authorContact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuthorContactExists(authorContact.AuthorId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAuthorContact", new { id = authorContact.AuthorId }, authorContact);
        }

        // DELETE: api/AuthorContacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorContact>> DeleteAuthorContact(long id)
        {
            var authorContact = await _context.AuthorContact.FindAsync(id);
            if (authorContact == null)
            {
                return NotFound();
            }

            _context.AuthorContact.Remove(authorContact);
            await _context.SaveChangesAsync();

            return authorContact;
        }

        private bool AuthorContactExists(long id)
        {
            return _context.AuthorContact.Any(e => e.AuthorId == id);
        }
    }
}
