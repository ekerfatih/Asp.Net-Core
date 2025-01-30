using BlogApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers {
    public class OgrenciController : Controller {

        private readonly DataContext _context;
        public OgrenciController(DataContext context) {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            var Ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(Ogrenciler);
        }

        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model) {

            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var ogrenci = await _context.Ogrenciler.Include(x => x.KursKayitlari).ThenInclude(x => x.Kurs).FirstOrDefaultAsync(o => o.Id == id);
            if (ogrenci == null) {
                return NotFound();
            }
            return View(ogrenci);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogrenci model) {

            if (id != model.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!_context.Ogrenciler.Any(o => o.Id == model.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int? id) {
            if (id == null) return NotFound();
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null) return NotFound();
            return View(ogrenci);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id) {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null) return NotFound();
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}