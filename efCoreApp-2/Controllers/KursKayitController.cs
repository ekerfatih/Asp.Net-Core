using BlogApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace BlogApp.Controllers {
    public class KursKayitController(DataContext context) : Controller {
        private readonly DataContext _context = context;

        public async Task<IActionResult> Create() {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "Id", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model) {
            model.KursKayitTarihi = DateTime.Now;
            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Index() {
            var kurskayitları = await _context.KursKayitlari.Include(x => x.Ogrenci).Include(y => y.Kurs).ToListAsync();
            return View(kurskayitları);
        }

    }
}