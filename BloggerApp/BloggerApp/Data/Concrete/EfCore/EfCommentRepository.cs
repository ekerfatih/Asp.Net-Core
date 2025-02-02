using BloggerApp.Data.Abstract;
using BloggerApp.Entity;

namespace BloggerApp.Data.Concrete.EfCore
{
    public class EfCommentRepository(BlogContext context) : ICommentRepository {
        private BlogContext _context = context;
        public IQueryable<Comment> Comments => _context.Comments;

        public void CreateComment(Comment comment) {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}