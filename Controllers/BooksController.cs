using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Highlights.Data;
using Highlights.Models;

namespace Highlights.Controllers
{
    public class BooksController : Controller
    {
        private readonly HighlightsContext _context;

        public BooksController(HighlightsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, string? topic)
        {
            // Is this the right thing? What is the standard?
            if (id == null || topic == null || _context.Book == null)
            {
                return NotFound();
            }

            ViewData["Topic"] = topic;
            ViewData["TopicId"] = id;           
            var highlightsContext = _context.Book.Where(b => b.TopicId == id);
            return View(await highlightsContext.ToListAsync());
        }
        
        public async Task<IActionResult> ForTopic(int? id, string? topic)
        {
            // Is this the right thing? What is thne standard?
            if (id == null || topic == null || _context.Book == null)
            {
                return NotFound();
            }

            ViewData["Topic"] = topic;
            ViewData["TopicId"] = id; 
            var highlightsContext = _context.Book.Where(b => b.TopicId == id);
            return View(await highlightsContext.ToListAsync());
        }
        
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create(int? id, string? topic)
        {
            ViewData["TopicId"] = id;
            ViewData["Topic"] = topic;
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, string? topic, [Bind("Title,Author,TopicId")] Book book)
        {
            // I have no clue why I need this or how it works
            ModelState.Remove("Topic");
            // ensures id is not null for safe casting
            if ( id != null && _context.Book != null && ModelState.IsValid)
            {
                book.TopicId = (int) id;
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["TopicId"] = id;
            ViewData["Topic"] = topic;
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", book.TopicId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,TopicId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", book.TopicId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'HighlightsContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
