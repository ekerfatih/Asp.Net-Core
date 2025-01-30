using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data {
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options) {
        public DbSet<Kurs> Kurslar => Set<Kurs>();
        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
        public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();
        public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>();
    }
}