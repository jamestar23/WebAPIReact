using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoEntriesController : ControllerBase
    {
        private readonly TodoDBContext _context;

        public TodoEntriesController(TodoDBContext context)
        {
            _context = context;
        }

        // GET: api/TodoEntries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoEntry>>> GetTodoEntries()
        {
            return await _context.TodoEntries.ToListAsync();
        }

        // GET: api/TodoEntries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoEntry>> GetTodoEntry(int id)
        {
            var todoEntry = await _context.TodoEntries.FindAsync(id);

            if (todoEntry == null)
            {
                return NotFound();
            }

            return todoEntry;
        }

        // PUT: api/TodoEntries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoEntry(int id, TodoEntry todoEntry)
        {
            todoEntry.id = id;

            _context.Entry(todoEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoEntryExists(id))
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

        // POST: api/TodoEntries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TodoEntry>> PostTodoEntry(TodoEntry todoEntry)
        {
            _context.TodoEntries.Add(todoEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoEntry", new { id = todoEntry.id }, todoEntry);
        }

        // DELETE: api/TodoEntries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoEntry>> DeleteTodoEntry(int id)
        {
            var todoEntry = await _context.TodoEntries.FindAsync(id);
            if (todoEntry == null)
            {
                return NotFound();
            }

            _context.TodoEntries.Remove(todoEntry);
            await _context.SaveChangesAsync();

            return todoEntry;
        }

        private bool TodoEntryExists(int id)
        {
            return _context.TodoEntries.Any(e => e.id == id);
        }
    }
}
