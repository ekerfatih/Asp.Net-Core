using BloggerApp.Data.Abstract;
using BloggerApp.Entity;

namespace BloggerApp.Data.Concrete.EfCore{
    public class EfTagRepository(BlogContext context) : ITagRepository {

        private readonly BlogContext _context = context;
        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag tag) {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }
    }
}