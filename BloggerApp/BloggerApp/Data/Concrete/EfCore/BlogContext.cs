using BloggerApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BloggerApp.Data.Concrete.EfCore {
    public class BlogContext(DbContextOptions<BlogContext> options) : DbContext(options) {
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // User ile Post arasındaki ilişki
            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // User silindiğinde Post'lar da silinsin

            // User ile Comment arasındaki ilişki
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // User silindiğinde Comment'ler silinmesin

            // Post ile Comment arasındaki ilişki
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Post silindiğinde Comment'ler de silinsin
        }
    }
}