using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab7.Models;

namespace lab7.Controllers
{
    public class TTurulsController : Controller
    {
        private readonly Lab7Context _context;

        public TTurulsController(Lab7Context context)
        {
            _context = context;
        }

        // GET: TTuruls
        public async Task<IActionResult> Index()
        {
            return View(await _context.TTuruls.ToListAsync());
        }

        // GET: TTuruls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTurul = await _context.TTuruls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tTurul == null)
            {
                return NotFound();
            }

            return View(tTurul);
        }

        // GET: TTuruls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TTuruls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TTurul tTurul)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tTurul);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tTurul);
        }

        // GET: TTuruls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTurul = await _context.TTuruls.FindAsync(id);
            if (tTurul == null)
            {
                return NotFound();
            }
            return View(tTurul);
        }

        // POST: TTuruls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TTurul tTurul)
        {
            if (id != tTurul.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTurul);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTurulExists(tTurul.Id))
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
            return View(tTurul);
        }

        // GET: TTuruls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTurul = await _context.TTuruls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tTurul == null)
            {
                return NotFound();
            }

            return View(tTurul);
        }

        // POST: TTuruls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tTurul = await _context.TTuruls.FindAsync(id);
            if (tTurul != null)
            {
                _context.TTuruls.Remove(tTurul);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TTurulExists(int id)
        {
            return _context.TTuruls.Any(e => e.Id == id);
        }
    }
}
