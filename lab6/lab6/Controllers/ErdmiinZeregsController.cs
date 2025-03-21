using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab6.Models;

namespace lab6.Controllers
{
    public class ErdmiinZeregsController : Controller
    {
        private readonly AppDbContext _context;

        public ErdmiinZeregsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ErdmiinZeregs
        public IActionResult Index()
        {
            var erdmiinZeregList = _context.ErdmiinZereguud.ToList();



            var viewModelList = erdmiinZeregList.Select(ez => new ViewModel
            {
                ErdmiinZereg = ez,
                Info = new Info()
            }).ToList();

            return View(viewModelList);
        }




        // GET: ErdmiinZeregs/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var erdmiinZereg = await _context.ErdmiinZereguud
                .FirstOrDefaultAsync(m => m.Id == id);
            if (erdmiinZereg == null)
            {
                return NotFound();
            }

            return View(erdmiinZereg);
        }
        // GET: Create
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new ViewModel
            {
                Info = new Info
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = User.Identity?.Name ?? "System"
                }
            };
            return View(viewModel);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ErdmiinZereg.Info = model.Info;

                _context.Add(model.ErdmiinZereg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        //[HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var erdmiinZereg = await _context.ErdmiinZereguud
                                               .Include(e => e.Info)
                                               .FirstOrDefaultAsync(m => m.Id == id);

            if (erdmiinZereg == null)
            {
                return NotFound();
            }

            var viewModel = new ViewModel
            {
                ErdmiinZereg = erdmiinZereg,
                Info = erdmiinZereg.Info // Info талбарыг зөв олгож байна.
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ViewModel model)
        {
            Console.WriteLine($"#################{model.ErdmiinZereg.Name}");
            if (id != model.ErdmiinZereg.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Info объектын мэдээлэл зөв шинэчлэгдэх ёстой
                    var existingEntity = await _context.ErdmiinZereguud.Include(e => e.Info)
                                                     .FirstOrDefaultAsync(m => m.Id == model.ErdmiinZereg.Id);
                    if (existingEntity != null)
                    {
                        existingEntity.Name = model.ErdmiinZereg.Name;
                        existingEntity.Info = model.Info; // Info-г шинэчлэнэ
                        _context.Update(existingEntity);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ErdmiinZeregExists(model.ErdmiinZereg.Id))
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
            return View(model);
        }

        // GET: ErdmiinZeregs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var erdmiinZereg = await _context.ErdmiinZereguud
                .FirstOrDefaultAsync(m => m.Id == id);
            if (erdmiinZereg == null)
            {
                return NotFound();
            }

            return View(erdmiinZereg);
        }

        // POST: ErdmiinZeregs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var erdmiinZereg = await _context.ErdmiinZereguud.FindAsync(id);
            if (erdmiinZereg != null)
            {
                _context.ErdmiinZereguud.Remove(erdmiinZereg);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ErdmiinZeregExists(int id)
        {
            return _context.ErdmiinZereguud.Any(e => e.Id == id);
        }
    }
}
