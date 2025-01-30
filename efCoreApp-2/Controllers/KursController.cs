using BlogApp.Data;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers {
    public class KursController(DataContext context) : Controller {

        private readonly DataContext _context = context;

        public async Task<IActionResult> CreateAsync() {
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
            var kurslar = await _context.Kurslar.Include(x => x.Ogretmen).ToListAsync();
            return View(kurslar);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) return NotFound();
            var kurs = await _context.Kurslar
                .Include(x => x.KursKayitlari)
                .ThenInclude(x => x.Ogrenci)
                .Select(k => new KursViewModel {
                    KursId = k.KursId,
                    Baslik = k.Baslik,
                    OgretmenId = k.OgretmenId,
                    KursKayitlari = k.KursKayitlari
                })
                .FirstOrDefaultAsync(x => x.KursId == id);
            if (kurs == null) return NotFound();
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(kurs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KursViewModel model) {

            if (id != model.KursId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(new Kurs() { KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException) {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) return NotFound();
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null) return NotFound();
            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null) return NotFound();
            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}