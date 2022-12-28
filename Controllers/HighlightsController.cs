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
    public class HighlightsController : Controller
    {
        private readonly HighlightsContext _context;

        public HighlightsController(HighlightsContext context)
        {
            _context = context;
        }

        // GET: Highlights
        public async Task<IActionResult> Index()
        {
            var highlightsContext = _context.Highlight.Include(h => h.Book).Include(h => h.Topic);
            return View(await highlightsContext.ToListAsync());
        }

        // GET: Highlights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Highlight == null)
            {
                return NotFound();
            }

            var highlight = await _context.Highlight
                .Include(h => h.Book)
                .Include(h => h.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (highlight == null)
            {
                return NotFound();
            }

            return View(highlight);
        }

        // GET: Highlights/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id");
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id");
            return View();
        }

        // POST: Highlights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,Comment,TopicId,BookId")] Highlight highlight)
        {
            // I have no clue why I need this or how it works
            ModelState.Remove("Topic");
            ModelState.Remove("Book");
            if (ModelState.IsValid)
            {
                _context.Add(highlight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", highlight.BookId);
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", highlight.TopicId);
            return View(highlight);
        }

        // GET: Highlights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Highlight == null)
            {
                return NotFound();
            }

            var highlight = await _context.Highlight.FindAsync(id);
            if (highlight == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", highlight.BookId);
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", highlight.TopicId);
            return View(highlight);
        }

        // POST: Highlights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,Comment,TopicId,BookId")] Highlight highlight)
        {
            if (id != highlight.Id)
            {
                return NotFound();
            }

            // I have no clue why I need this or how it works
            ModelState.Remove("Topic");
            ModelState.Remove("Book");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(highlight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HighlightExists(highlight.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", highlight.BookId);
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Id", highlight.TopicId);
            return View(highlight);
        }

        // GET: Highlights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Highlight == null)
            {
                return NotFound();
            }

            var highlight = await _context.Highlight
                .Include(h => h.Book)
                .Include(h => h.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (highlight == null)
            {
                return NotFound();
            }

            return View(highlight);
        }

        // POST: Highlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Highlight == null)
            {
                return Problem("Entity set 'HighlightsContext.Highlight'  is null.");
            }
            var highlight = await _context.Highlight.FindAsync(id);
            if (highlight != null)
            {
                _context.Highlight.Remove(highlight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HighlightExists(int id)
        {
          return (_context.Highlight?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
