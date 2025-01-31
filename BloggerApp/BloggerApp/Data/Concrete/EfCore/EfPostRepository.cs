using BloggerApp.Data.Abstract;
using BloggerApp.Entity;

namespace BloggerApp.Data.Concrete.EfCore{
    public class EfPostRepository(BlogContext context) : IPostRepository {

        private readonly BlogContext _context = context;
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post) {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}