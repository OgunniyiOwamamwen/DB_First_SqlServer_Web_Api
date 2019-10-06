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
    //1.api/[controller]
    [Route("/[action]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public PublishersController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/Publishers
        [HttpGet]
        //[AcceptVerbs("GET", "HEAD")]
        [ActionName("GetAllPublisher")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublisher()
        {
            return await _context.Publisher.ToListAsync();
        }

        // GET: api/Publishers/5
        //[HttpGet("{id}")]
        [HttpGet]
        //Added
        [ActionName("PublisherById")]
        public async Task<ActionResult<Publisher>> GetPublisher(long id)
        {
            var publisher = await _context.Publisher.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        // PUT: api/Publishers/5
        //[HttpPut("{id}")]
        [HttpPut]
        [ActionName("UpdatePublisher")]
        public async Task<IActionResult> PutPublisher(long id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        // POST: api/Publishers
        [HttpPost]
        [ActionName("InsertPublisher")]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
            _context.Publisher.Add(publisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublisher", new { id = publisher.Id }, publisher);
        }

        // DELETE: api/Publishers/5
        //[HttpDelete("{id}")]
        [HttpDelete]
        [ActionName("DeletePublisher")]
        public async Task<ActionResult<Publisher>> DeletePublisher(long id)
        {
            var publisher = await _context.Publisher.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();

            return publisher;
        }

        private bool PublisherExists(long id)
        {
            return _context.Publisher.Any(e => e.Id == id);
        }
    }
}
