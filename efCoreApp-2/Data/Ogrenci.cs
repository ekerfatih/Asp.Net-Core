using System.ComponentModel.DataAnnotations;

namespace BlogApp.Data {
    public class Ogrenci {
        [Display(Name = "Öğrenci No")]
        public int Id { get; set; }

        [Display(Name = "Öğrenci Ad")]
        public string? OgrenciAd { get; set; }
        [Display(Name = "Öğrenci Soyad")]
        public string? OgrenciSoyad { get; set; }

        public string AdSoyad {
            get {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            }
        }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
    }
}