using lab11_12.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab11_12.Controllers
{
    public class TenhimController : Controller
    {
        private readonly Lab12Context _context;

        public TenhimController(Lab12Context context)
        {
            _context = context;
        }
        // GET: TenhimController
        public ActionResult Index()
        {
            var tenhims = _context.TTenhims.ToList();
            return View(tenhims);
        }

        // GET: TenhimController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

    
       
        // GET: TenhimController/Edit/5
        public ActionResult Edit(int id, string[] seletedMergejil)
        {
            TTenhim tenhim = _context.TTenhims.Include(t => t.TMergejils).Where(t => t.Id == id).Single();
            var songogdsonMergejil = new List<CheckMergejil>();
            var mcodes = new HashSet<string>(tenhim.TMergejils.Select(m => m.Mid));

            foreach (var mergejil in _context.TMergejils)
            {
                songogdsonMergejil.Add(new CheckMergejil
                {
                    mcode = mergejil.Mid,
                    mname = mergejil.Name,
                    isChecked = mcodes.Contains(mergejil.Mid)
                });
            }

            ViewBag.CheckMergejil = songogdsonMergejil;

            return View(tenhim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string name, string[] seletedMergejil)
        {
            var tenhimToUpdate = _context.TTenhims
                .Include(t => t.TMergejils)
                .FirstOrDefault(t => t.Id == id);

            if (tenhimToUpdate == null)
            {
                return NotFound();
            }

            tenhimToUpdate.Name = name;

            var selectedMergejilSet = new HashSet<string>(seletedMergejil ?? new string[] { });
            var currentMergejils = new HashSet<string>(tenhimToUpdate.TMergejils.Select(m => m.Mid));

            foreach (var mergejil in _context.TMergejils)
            {
                if (selectedMergejilSet.Contains(mergejil.Mid))
                {
                    if (!currentMergejils.Contains(mergejil.Mid))
                    {
                        tenhimToUpdate.TMergejils.Add(mergejil);
                    }
                }
                else
                {
                    if (currentMergejils.Contains(mergejil.Mid))
                    {
                        tenhimToUpdate.TMergejils.Remove(mergejil);
                    }
                }
            }

            try
            {
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // 💡 ViewBag-д Mergejil-уудыг дахин онооно
                var songogdsonMergejil = new List<CheckMergejil>();
                foreach (var mergejil in _context.TMergejils)
                {
                    songogdsonMergejil.Add(new CheckMergejil
                    {
                        mcode = mergejil.Mid,
                        mname = mergejil.Name,
                        isChecked = selectedMergejilSet.Contains(mergejil.Mid)
                    });
                }
                ViewBag.CheckMergejil = songogdsonMergejil;

                return View(tenhimToUpdate);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var mergejils = _context.TMergejils.ToList();
            var checkMergejils = mergejils.Select(m => new CheckMergejil
            {
                mcode = m.Mid,
                mname = m.Name,
                isChecked = false
            }).ToList();

            ViewBag.CheckMergejil = checkMergejils;

            return View();
        }

        // POST: Tenhim/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TTenhim tenhim, string[] seletedMergejil)
        {
            if (ModelState.IsValid)
            {
                tenhim.TMergejils = new List<TMergejil>();
                var selectedSet = new HashSet<string>(seletedMergejil ?? new string[] { });

                foreach (var mergejil in _context.TMergejils)
                {
                    if (selectedSet.Contains(mergejil.Mid))
                    {
                        tenhim.TMergejils.Add(mergejil);
                    }
                }

                _context.TTenhims.Add(tenhim);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Хэрвээ ModelState буруу байвал checkbox-уудыг дахин онооно
            var checkMergejils = _context.TMergejils.Select(m => new CheckMergejil
            {
                mcode = m.Mid,
                mname = m.Name,
                isChecked = seletedMergejil.Contains(m.Mid)
            }).ToList();

            ViewBag.CheckMergejil = checkMergejils;

            return View(tenhim);
        }

        // GET: Tenhim/Delete/5
        public IActionResult Delete(int id)
        {
            var tenhim = _context.TTenhims.Include(t => t.TMergejils).FirstOrDefault(t => t.Id == id);
            if (tenhim == null)
            {
                return NotFound();
            }
            return View(tenhim);
        }

        // POST: Tenhim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tenhim = _context.TTenhims.Include(t => t.TMergejils).FirstOrDefault(t => t.Id == id);
            if (tenhim != null)
            {
                tenhim.TMergejils.Clear();
                _context.TTenhims.Remove(tenhim);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
