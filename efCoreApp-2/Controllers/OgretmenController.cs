using BlogApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BlogApp.Controllers {
    public class OgretmenController(DataContext context) : Controller {
        private readonly DataContext _context = context;

        public async Task<IActionResult> Index() {
            var Ogrenciler = await _context.Ogretmenler.ToListAsync();
            return View(Ogrenciler);
        }

        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model) {

            _context.Ogretmenler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var ogretmen = await _context.Ogretmenler.FirstOrDefaultAsync(o => o.OgretmenId == id);
            if (ogretmen == null) {
                return NotFound();
            }
            return View(ogretmen);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogretmen model) {

            if (id != model.OgretmenId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}