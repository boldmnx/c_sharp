using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using delguur.Data;
using delguur.Models;
using Microsoft.AspNetCore.Authorization;

namespace delguur.Controllers
{
    public class CeramicProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CeramicProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CeramicProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.CeramicProducts.ToListAsync());
        }

        // GET: CeramicProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ceramicProduct = await _context.CeramicProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ceramicProduct == null)
            {
                return NotFound();
            }

            return View(ceramicProduct);
        }

        // GET: CeramicProducts/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Category,ImageUrl,StockQuantity,CreatedAt")] CeramicProduct ceramicProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ceramicProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ceramicProduct);
        }

        // GET: CeramicProducts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ceramicProduct = await _context.CeramicProducts.FindAsync(id);
            if (ceramicProduct == null)
            {
                return NotFound();
            }
            return View(ceramicProduct);
        }

        // POST: CeramicProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Category,ImageUrl,StockQuantity,CreatedAt")] CeramicProduct ceramicProduct)
        {
            if (id != ceramicProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ceramicProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CeramicProductExists(ceramicProduct.Id))
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
            return View(ceramicProduct);
        }

        // GET: CeramicProducts/Delete/5

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ceramicProduct = await _context.CeramicProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ceramicProduct == null)
            {
                return NotFound();
            }

            return View(ceramicProduct);
        }
        [Authorize(Roles = "Admin")]

        // POST: CeramicProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ceramicProduct = await _context.CeramicProducts.FindAsync(id);
            if (ceramicProduct != null)
            {
                _context.CeramicProducts.Remove(ceramicProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CeramicProductExists(int id)
        {
            return _context.CeramicProducts.Any(e => e.Id == id);
        }
    }
}
