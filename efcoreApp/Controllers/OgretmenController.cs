using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers {
    public class OgretmenController : Controller {
        private readonly DataContext _context;
        public OgretmenController(DataContext context) {
            _context = context;
        }

        public async Task<ActionResult> Index() {
            var ogretmenler = await _context.Ogretmenler.ToListAsync();
            return View(ogretmenler);
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

        public async Task<ActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }
            var ogretmen = await _context
                .Ogretmenler
                .FirstOrDefaultAsync(x => x.OgretmenId == id);

            if (ogretmen == null) {
                return NotFound();
            }

            return View(ogretmen);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, Ogretmen model) {
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