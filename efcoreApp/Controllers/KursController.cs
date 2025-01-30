using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers {
    public class KursController : Controller {

        private readonly DataContext _context;

        public KursController(DataContext dataContext) {
            _context = dataContext;
        }

        public async Task<IActionResult> Create() {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Kurs model) {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index() {

            var kurslar = await _context.Kurslar.Include(k => k.Ogretmen).ToListAsync();
            return View(kurslar);
        }


        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }
            var kurs = await _context
                .Kurslar
                .Include(x => x.KursKayitlari)
                .ThenInclude(x => x.Ogrenci)
                .FirstOrDefaultAsync(o => o.KursId == id);

            if (kurs == null) {
                return NotFound();
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(kurs);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, Kurs model) {
            if (id != model.KursId) {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null) {
                return NotFound();
            }
            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id) {
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null) return NotFound();
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}